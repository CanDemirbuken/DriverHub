using DriverHub.Application.Features.Entities.Reservations.Queries.GetAvailableCars;

using DriverHub.Application.Common.Models;
using DriverHub.Application.Features.Entities.Reservations.Common;
using DriverHub.Application.Features.Entities.Reservations.Queries.GetPagedReservations;

namespace DriverHub.Application.Interfaces.QueryServices;

public interface IReservationQueryService
{
    Task<PagedResponse<ReservationResponse>> GetPagedAsync(
        GetPagedReservationsQuery request,
        CancellationToken cancellationToken = default);
    Task<ReservationResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> LocationExistsAsync(Guid locationId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<GetAvailableCarsQueryResponse>> GetAvailableCarsAsync(
        Guid pickupLocationId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default);

    Task<bool> IsCarAvailableAsync(Guid carId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
}
