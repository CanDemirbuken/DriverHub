using DriverHub.Application.Features.Entities.Reservations.Queries.GetAvailableCars;

namespace DriverHub.Application.Interfaces.QueryServices;

public interface IReservationQueryService
{
    Task<bool> LocationExistsAsync(Guid locationId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<GetAvailableCarsQueryResponse>> GetAvailableCarsAsync(
        Guid pickupLocationId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default);

    Task<bool> IsCarAvailableAsync(Guid carId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
}
