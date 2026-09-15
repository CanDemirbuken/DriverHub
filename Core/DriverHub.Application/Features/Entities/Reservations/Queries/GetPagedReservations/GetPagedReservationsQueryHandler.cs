using DriverHub.Application.Common.Models;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.Reservations.Common;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.Entities.Reservations.Queries.GetPagedReservations;

public sealed class GetPagedReservationsQueryHandler(IReservationQueryService queryService)
    : IRequestHandler<GetPagedReservationsQuery, Result<PagedResponse<ReservationResponse>>>
{
    public async Task<Result<PagedResponse<ReservationResponse>>> Handle(GetPagedReservationsQuery request, CancellationToken cancellationToken) =>
        Result<PagedResponse<ReservationResponse>>.Success(await queryService.GetPagedAsync(request, cancellationToken));
}
