using DriverHub.Application.Common.Errors;
using DriverHub.Application.Features.Entities.Reservations.Commands.UpdateReservationStatus;
using DriverHub.Application.Features.Entities.Reservations.Queries.GetPagedReservations;
using DriverHub.Domain.Entities;
using DriverHub.Domain.Enums;
using DriverHub.Persistence.Communication;
using DriverHub.Persistence.Context;
using DriverHub.Persistence.QueryServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverHub.Tests;

public sealed class ReservationIntegrationTests(ReservationDatabase database) : IClassFixture<ReservationDatabase>
{
    [Fact]
    public async Task Creation_persists_pending_customer_snapshot_prices_and_email_atomically()
    {
        await using var context = database.CreateContext();
        var seed = await database.SeedAsync(context);
        var result = await ReservationDatabase.CreateHandler(context).Handle(seed.Command, default);
        Assert.True(result.IsSuccess);
        context.ChangeTracker.Clear();
        var reservation = await context.Set<Reservation>().SingleAsync(r => r.Id == result.Value.Id);
        Assert.Equal(ReservationStatus.Pending, reservation.Status);
        Assert.Equal(seed.UserId, reservation.UserId);
        Assert.Equal("Ada", reservation.CustomerFirstName);
        Assert.Equal("Lovelace", reservation.CustomerLastName);
        Assert.Equal("+905550000000", reservation.CustomerPhone);
        Assert.Equal(300m, reservation.TotalPrice);
        Assert.Single(await context.Set<EmailOutboxMessage>().Where(m => m.ReservationId == reservation.Id).ToListAsync());

        var user = await context.Users.FindAsync(seed.UserId);
        user!.FirstName = "Changed";
        await context.SaveChangesAsync();
        var detail = await new ReservationQueryService(context).GetByIdAsync(reservation.Id);
        Assert.Equal("Ada", detail!.CustomerFirstName);
        Assert.Equal(3, detail.RentalDays);
        Assert.Equal(DateTimeKind.Utc, detail.StartDate.Kind);
    }

    [Theory]
    [InlineData(ReservationStatus.Confirmed)] [InlineData(ReservationStatus.Cancelled)]
    public async Task Pending_transition_is_idempotent_and_audited(ReservationStatus status)
    {
        await using var context = database.CreateContext();
        var seed = await database.SeedAsync(context);
        var created = await ReservationDatabase.CreateHandler(context).Handle(seed.Command, default);
        context.ChangeTracker.Clear();
        var command = new UpdateReservationStatusCommand(created.Value.Id, status, seed.UserId);
        Assert.True((await ReservationDatabase.StatusHandler(context).Handle(command, default)).IsSuccess);
        var reservation = await context.Set<Reservation>().FindAsync(created.Value.Id);
        var processed = reservation!.ProcessedAt;
        Assert.NotNull(processed);
        Assert.Equal(seed.UserId, reservation.ProcessedBy);
        context.ChangeTracker.Clear();
        Assert.True((await ReservationDatabase.StatusHandler(context).Handle(command, default)).IsSuccess);
        Assert.Equal(processed, (await context.Set<Reservation>().FindAsync(created.Value.Id))!.ProcessedAt);
        Assert.Equal(2, await context.Set<EmailOutboxMessage>().CountAsync(m => m.ReservationId == created.Value.Id));
    }

    [Theory]
    [InlineData(ReservationStatus.Cancelled, ReservationStatus.Confirmed)]
    [InlineData(ReservationStatus.Confirmed, ReservationStatus.Cancelled)]
    [InlineData(ReservationStatus.Completed, ReservationStatus.Confirmed)]
    public async Task Invalid_transition_is_rejected(ReservationStatus current, ReservationStatus target)
    {
        await using var context = database.CreateContext();
        var seed = await database.SeedAsync(context);
        var reservation = MakeReservation(seed, current);
        context.Add(reservation);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        var result = await ReservationDatabase.StatusHandler(context).Handle(new(reservation.Id, target, seed.UserId), default);
        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.Conflict, result.Errors.Single().Type);
        Assert.Empty(await context.Set<EmailOutboxMessage>().Where(m => m.ReservationId == reservation.Id).ToListAsync());
    }

    [Fact]
    public async Task Invalid_id_and_inactive_customer_are_rejected()
    {
        await using var context = database.CreateContext();
        var seed = await database.SeedAsync(context);
        var missing = await ReservationDatabase.StatusHandler(context).Handle(new(Guid.NewGuid(), ReservationStatus.Confirmed, seed.UserId), default);
        Assert.Equal(ErrorType.NotFound, missing.Errors.Single().Type);
        var invalidUser = await ReservationDatabase.CreateHandler(context).Handle(seed.Command with { UserId = "missing" }, default);
        Assert.Equal(ErrorType.Unauthorized, invalidUser.Errors.Single().Type);
        var invalidCar = await ReservationDatabase.CreateHandler(context).Handle(seed.Command with { CarId = Guid.NewGuid() }, default);
        Assert.Equal(ErrorType.NotFound, invalidCar.Errors.Single().Type);
        var invalidLocation = await ReservationDatabase.CreateHandler(context).Handle(seed.Command with { PickupLocationId = Guid.NewGuid() }, default);
        Assert.Equal(ErrorType.NotFound, invalidLocation.Errors.Single().Type);
    }

    [Theory]
    [InlineData(true)] [InlineData(false)]
    public async Task Approval_rechecks_expired_dates_and_inactive_car(bool expired)
    {
        await using var context = database.CreateContext();
        var seed = await database.SeedAsync(context);
        var reservation = MakeReservation(seed, ReservationStatus.Pending);
        if (expired) { reservation.StartDate = DateTime.UtcNow.AddDays(-2); reservation.EndDate = DateTime.UtcNow.AddDays(-1); }
        else (await context.Set<Car>().FindAsync(seed.CarId))!.Status = (CarStatus)2;
        context.Add(reservation);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        var result = await ReservationDatabase.StatusHandler(context).Handle(new(reservation.Id, ReservationStatus.Confirmed, seed.UserId), default);
        Assert.Equal(ErrorType.Conflict, result.Errors.Single().Type);
    }

    [Fact]
    public async Task Simultaneous_creations_for_same_car_allow_only_one_and_adjacent_dates_remain_available()
    {
        ReservationSeed seed;
        await using (var setup = database.CreateContext()) seed = await database.SeedAsync(setup);
        async Task<bool> Create()
        {
            await using var context = database.CreateContext();
            return (await ReservationDatabase.CreateHandler(context).Handle(seed.Command, default)).IsSuccess;
        }
        var results = await Task.WhenAll(Create(), Create());
        Assert.Single(results, success => success);
        await using var check = database.CreateContext();
        var query = new ReservationQueryService(check);
        Assert.False(await query.IsCarAvailableAsync(seed.CarId, seed.Start, seed.Start.AddDays(3)));
        Assert.True(await query.IsCarAvailableAsync(seed.CarId, seed.Start.AddDays(3), seed.Start.AddDays(4)));
    }

    [Fact]
    public async Task Two_admins_approving_same_reservation_enqueue_only_one_confirmation()
    {
        ReservationSeed seed;
        Guid id;
        await using (var setup = database.CreateContext())
        {
            seed = await database.SeedAsync(setup);
            id = (await ReservationDatabase.CreateHandler(setup).Handle(seed.Command, default)).Value.Id;
        }
        async Task<bool> Approve()
        {
            await using var context = database.CreateContext();
            return (await ReservationDatabase.StatusHandler(context).Handle(new(id, ReservationStatus.Confirmed, seed.UserId), default)).IsSuccess;
        }
        Assert.All(await Task.WhenAll(Approve(), Approve()), Assert.True);
        await using var check = database.CreateContext();
        Assert.Equal(2, await check.Set<EmailOutboxMessage>().CountAsync(m => m.ReservationId == id));
    }

    [Fact]
    public async Task Conflicting_legacy_pending_requests_cannot_both_be_approved()
    {
        ReservationSeed seed;
        Guid firstId, secondId;
        await using (var setup = database.CreateContext())
        {
            seed = await database.SeedAsync(setup);
            var first = MakeReservation(seed, ReservationStatus.Pending);
            var second = MakeReservation(seed, ReservationStatus.Pending);
            firstId = first.Id; secondId = second.Id;
            setup.AddRange(first, second);
            await setup.SaveChangesAsync();
        }
        async Task<bool> Approve(Guid id)
        {
            await using var context = database.CreateContext();
            return (await ReservationDatabase.StatusHandler(context).Handle(new(id, ReservationStatus.Confirmed, seed.UserId), default)).IsSuccess;
        }
        Assert.All(await Task.WhenAll(Approve(firstId), Approve(secondId)), success => Assert.False(success));
        // Existing domain blocks overlapping Pending records too; cancel one to resolve the conflict.
        await using var check = database.CreateContext();
        Assert.True((await ReservationDatabase.StatusHandler(check).Handle(new(secondId, ReservationStatus.Cancelled, seed.UserId), default)).IsSuccess);
        check.ChangeTracker.Clear();
        Assert.True((await ReservationDatabase.StatusHandler(check).Handle(new(firstId, ReservationStatus.Confirmed, seed.UserId), default)).IsSuccess);
    }

    [Fact]
    public async Task Paged_query_applies_filters_and_stable_order_in_database()
    {
        await using var context = database.CreateContext();
        var seed = await database.SeedAsync(context);
        var one = MakeReservation(seed, ReservationStatus.Pending);
        var two = MakeReservation(seed, ReservationStatus.Cancelled);
        context.AddRange(one, two);
        await context.SaveChangesAsync();
        var query = new ReservationQueryService(context);
        var page = await query.GetPagedAsync(new(PageSize: 1, CarId: seed.CarId));
        Assert.Single(page.Items);
        Assert.Equal(2, page.TotalCount);
        var filtered = await query.GetPagedAsync(new(CarId: seed.CarId, Status: ReservationStatus.Pending, Search: "Ada", From: seed.Start, To: seed.Start.AddDays(1)));
        Assert.Equal(one.Id, Assert.Single(filtered.Items).Id);
    }

    [Fact]
    public async Task Outbox_retry_and_delivery_receipt_do_not_change_reservation_state()
    {
        await using var context = database.CreateContext();
        var seed = await database.SeedAsync(context);
        var result = await ReservationDatabase.CreateHandler(context).Handle(seed.Command, default);
        var message = await context.Set<EmailOutboxMessage>().SingleAsync(m => m.ReservationId == result.Value.Id);
        var outbox = new EmailOutbox(context);
        await outbox.ScheduleRetryAsync(message.Id, default);
        await context.SaveChangesAsync();
        Assert.Equal(1, message.Attempts);
        Assert.True(message.NextAttemptAt > DateTime.UtcNow);
        Assert.Null(message.DeliveredAt);
        await outbox.MarkDeliveredAsync(message.Id, default);
        await context.SaveChangesAsync();
        Assert.NotNull(message.DeliveredAt);
        Assert.Empty(message.Body);
        Assert.Equal(ReservationStatus.Pending, (await context.Set<Reservation>().FindAsync(result.Value.Id))!.Status);
        await using var transaction = await context.BeginTransactionAsync();
        // Executes the production SQL claim query on SQL Server, without connecting to SMTP.
        await outbox.GetNextForDeliveryAsync(default);
    }

    [Fact]
    public async Task Migration_upgrades_existing_reservation_without_changing_status_price_or_audit()
    {
        var legacy = new ReservationDatabase();
        try
        {
            await using var context = legacy.CreateContext();
            var migrator = context.GetService<IMigrator>();
            await migrator.MigrateAsync("20260815211025_RemoveLegacyContentEntities");
            var seed = await legacy.SeedAsync(context);
            Guid id = Guid.NewGuid();
            await context.Database.ExecuteSqlInterpolatedAsync($"""
                INSERT INTO Reservations (Id, UserId, CarId, PickupLocationId, ReturnLocationId, StartDate, EndDate, Status,
                  BasePrice, ExtraPrice, InsurancePrice, TotalPrice, CreatedDate)
                VALUES ({id}, {seed.UserId}, {seed.CarId}, {seed.LocationId}, {seed.LocationId}, {seed.Start}, {seed.Start.AddDays(3)},
                  {2}, {300m}, {0m}, {0m}, {300m}, {DateTime.UtcNow})
                """);
            await migrator.MigrateAsync();
            var reservation = await context.Set<Reservation>().SingleAsync(r => r.Id == id);
            Assert.Equal(ReservationStatus.Confirmed, reservation.Status);
            Assert.Equal(300m, reservation.TotalPrice);
            Assert.Equal("Ada", reservation.CustomerFirstName);
            Assert.Null(reservation.ProcessedAt);
            Assert.Null(reservation.ProcessedBy);
            Assert.False(context.Database.HasPendingModelChanges());
        }
        finally { await legacy.DisposeAsync(); }
    }

    private static Reservation MakeReservation(ReservationSeed seed, ReservationStatus status) => new()
    {
        UserId = seed.UserId, CarId = seed.CarId, PickupLocationId = seed.LocationId, ReturnLocationId = seed.LocationId,
        StartDate = seed.Start, EndDate = seed.Start.AddDays(3), Status = status,
        CustomerFirstName = "Ada", CustomerLastName = "Lovelace", CustomerEmail = seed.UserId + "@example.test",
        BasePrice = 300, TotalPrice = 300
    };
}
