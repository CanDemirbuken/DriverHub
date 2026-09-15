using DriverHub.Application.Features.Entities.Reservations.Queries.GetAvailableCars;
using DriverHub.Application.Common.Models;
using DriverHub.Application.Features.Entities.Reservations.Common;
using DriverHub.Application.Features.Entities.Reservations.Queries.GetPagedReservations;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Domain.Entities;
using DriverHub.Domain.Enums;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.QueryServices;

public sealed class ReservationQueryService(AppDbContext context) : IReservationQueryService
{
    public async Task<PagedResponse<ReservationResponse>> GetPagedAsync(GetPagedReservationsQuery request, CancellationToken cancellationToken = default)
    {
        IQueryable<Reservation> query = context.Set<Reservation>().AsNoTracking();
        if (request.Status.HasValue) query = query.Where(r => r.Status == request.Status);
        if (request.CarId.HasValue) query = query.Where(r => r.CarId == request.CarId);
        // Rental periods intersect the supplied half-open interval [From, To).
        if (request.From.HasValue) query = query.Where(r => r.EndDate > request.From);
        if (request.To.HasValue) query = query.Where(r => r.StartDate < request.To);
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            string search = request.Search.Trim();
            Guid.TryParse(search, out Guid reservationId);
            query = query.Where(r => r.Id == reservationId ||
                ((r.CustomerFirstName ?? "") + " " + (r.CustomerLastName ?? "")).Contains(search) ||
                (r.CustomerEmail != null && r.CustomerEmail.Contains(search)) ||
                r.Car!.Plate.Contains(search) || r.Car.Model.Contains(search) || r.Car.Brand!.Name.Contains(search));
        }

        int count = await query.CountAsync(cancellationToken);
        query = request.OldestFirst
            ? query.OrderBy(r => r.CreatedDate).ThenBy(r => r.Id)
            : query.OrderByDescending(r => r.CreatedDate).ThenByDescending(r => r.Id);
        var items = await Project(query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize))
            .ToListAsync(cancellationToken);
        return PagedResponse<ReservationResponse>.CreateResponse(items, request.PageNumber, request.PageSize, count);
    }

    public Task<ReservationResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        Project(context.Set<Reservation>().AsNoTracking().Where(r => r.Id == id)).SingleOrDefaultAsync(cancellationToken);

    private IQueryable<ReservationResponse> Project(IQueryable<Reservation> query) =>
        query.Select(r => new ReservationResponse(
            r.Id, r.CarId, r.Car!.Brand!.Name, r.Car.Model, r.Car.Plate,
            r.CustomerFirstName, r.CustomerLastName, r.CustomerEmail, r.CustomerPhone,
            r.PickupLocationId, context.Set<Location>().Where(l => l.Id == r.PickupLocationId).Select(l => l.Name).First(),
            r.ReturnLocationId, context.Set<Location>().Where(l => l.Id == r.ReturnLocationId).Select(l => l.Name).First(),
            DateTime.SpecifyKind(r.StartDate, DateTimeKind.Utc), DateTime.SpecifyKind(r.EndDate, DateTimeKind.Utc),
            DateTime.SpecifyKind(r.CreatedDate, DateTimeKind.Utc), r.Status,
            r.ProcessedAt.HasValue ? DateTime.SpecifyKind(r.ProcessedAt.Value, DateTimeKind.Utc) : null,
            context.Users.Where(u => u.Id == r.ProcessedBy).Select(u => u.FirstName + " " + u.LastName).FirstOrDefault(),
            r.BasePrice, r.ExtraPrice, r.InsurancePrice, r.TotalPrice));

    public Task<bool> LocationExistsAsync(Guid locationId, CancellationToken cancellationToken = default) =>
        context.Set<Location>().AnyAsync(location => location.Id == locationId, cancellationToken);

    public async Task<IReadOnlyList<GetAvailableCarsQueryResponse>> GetAvailableCarsAsync(
        Guid pickupLocationId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        return await context.Set<Car>()
            .AsNoTracking()
            .Where(car =>
                car.Status == CarStatus.Active &&
                car.CurrentLocationId == pickupLocationId &&
                !car.Reservations.Any(reservation =>
                    (reservation.Status == ReservationStatus.Pending || reservation.Status == ReservationStatus.Confirmed) &&
                    reservation.StartDate < endDate &&
                    reservation.EndDate > startDate))
            .OrderBy(car => car.Brand!.Name)
            .ThenBy(car => car.Model)
            .Select(car => new GetAvailableCarsQueryResponse(
                car.Id, car.CoverImageUrl, car.Brand!.Name, car.Model, car.ModelYear,
                car.Plate, car.Category!.Name, car.CurrentLocation!.Name, car.Km,
                car.Transmission, car.Fuel, car.Status))
            .ToListAsync(cancellationToken);
    }

    public Task<bool> IsCarAvailableAsync(Guid carId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default) =>
        context.Set<Car>().AnyAsync(car =>
            car.Id == carId &&
            car.Status == CarStatus.Active &&
            !car.Reservations.Any(reservation =>
                (reservation.Status == ReservationStatus.Pending || reservation.Status == ReservationStatus.Confirmed) &&
                reservation.StartDate < endDate &&
                reservation.EndDate > startDate), cancellationToken);
}
