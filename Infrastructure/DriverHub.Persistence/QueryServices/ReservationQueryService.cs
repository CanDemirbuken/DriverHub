using DriverHub.Application.Features.Entities.Reservations.Queries.GetAvailableCars;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Domain.Entities;
using DriverHub.Domain.Enums;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.QueryServices;

public sealed class ReservationQueryService(AppDbContext context) : IReservationQueryService
{
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
