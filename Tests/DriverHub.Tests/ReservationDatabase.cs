using DriverHub.Application.Extensions;
using DriverHub.Application.Features.Entities.Reservations.Commands.CreateReservation;
using DriverHub.Application.Features.Entities.Reservations.Commands.UpdateReservationStatus;
using DriverHub.Application.Features.Entities.Reservations.Common;
using DriverHub.Application.Interfaces.QueryServices.Identity;
using DriverHub.Domain.Entities;
using DriverHub.Domain.Enums;
using DriverHub.Persistence.Communication;
using DriverHub.Persistence.Context;
using DriverHub.Persistence.Identity;
using DriverHub.Persistence.QueryServices;
using DriverHub.Persistence.QueryServices.Identity;
using DriverHub.Persistence.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Tests;

public sealed class ReservationDatabase : IAsyncLifetime
{
    // Always create a new, disposable database. Never use application configuration.
    public string ConnectionString { get; } = new SqlConnectionStringBuilder
    {
        DataSource = Environment.GetEnvironmentVariable("DRIVERHUB_TEST_SQL_SERVER") ?? @"(localdb)\MSSQLLocalDB",
        InitialCatalog = "DriverHub_ReservationTests_" + Guid.NewGuid().ToString("N"),
        IntegratedSecurity = true, TrustServerCertificate = true, ConnectTimeout = 15
    }.ConnectionString;

    public AppDbContext CreateContext() => new(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(ConnectionString).Options);

    public async Task InitializeAsync()
    {
        await using var context = CreateContext();
        await context.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        var builder = new SqlConnectionStringBuilder(ConnectionString);
        if (!builder.InitialCatalog.StartsWith("DriverHub_ReservationTests_", StringComparison.Ordinal))
            throw new InvalidOperationException("Refusing to delete a non-test database.");
        await using var context = CreateContext();
        await context.Database.EnsureDeletedAsync();
    }

    public async Task<ReservationSeed> SeedAsync(AppDbContext context)
    {
        string suffix = Guid.NewGuid().ToString("N");
        var user = new AppUser { Id = suffix, UserName = suffix, FirstName = "Ada", LastName = "Lovelace", Email = suffix + "@example.test", PhoneNumber = "+905550000000", IsActive = true, CreatedDate = DateTime.UtcNow };
        var location = new Location { Name = "Airport " + suffix };
        var car = new Car
        {
            Brand = new Brand { Name = "Brand " + suffix }, Category = new Category { Name = "Category " + suffix },
            CurrentLocation = location, Model = "Test Model", ModelYear = 2025, Seat = 5,
            Plate = suffix[..15], Vin = suffix[..17], Status = CarStatus.Active,
            CarPricings = [new() { Type = PricingType.Daily, Amount = 100 }, new() { Type = PricingType.Weekly, Amount = 600 }, new() { Type = PricingType.Monthly, Amount = 2000 }]
        };
        context.Add(user);
        context.Add(car);
        await context.SaveChangesAsync();
        return new(user.Id, car.Id, location.Id, DateTime.UtcNow.AddDays(2));
    }

    public static CreateReservationCommandHandler CreateHandler(AppDbContext context) => new(
        new Repository<Location>(context), new ReservationRepository(context), new Repository<Extra>(context),
        new Repository<InsurancePackage>(context), new Repository<ReservationExtra>(context), new CarQueryService(context),
        new UserQueryService(context), new ReservationCustomerValidator(), new EmailOutbox(context), context);

    public static UpdateReservationStatusCommandHandler StatusHandler(AppDbContext context) => new(
        new ReservationRepository(context), new ReservationQueryService(context), new UserQueryService(context), new EmailOutbox(context), context);
}

public sealed record ReservationSeed(string UserId, Guid CarId, Guid LocationId, DateTime Start)
{
    public CreateReservationCommand Command => new(UserId, CarId, LocationId, Start, Start.AddDays(3), [], null);
}
