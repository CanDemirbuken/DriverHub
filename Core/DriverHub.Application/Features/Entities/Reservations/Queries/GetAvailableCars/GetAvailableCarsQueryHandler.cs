using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.Entities.Reservations.Queries.GetAvailableCars;

public sealed class GetAvailableCarsQueryHandler(IReservationQueryService reservationQueryService) : IRequestHandler<GetAvailableCarsQuery, Result<IReadOnlyList<GetAvailableCarsQueryResponse>>>
{
    public async Task<Result<IReadOnlyList<GetAvailableCarsQueryResponse>>> Handle(GetAvailableCarsQuery request, CancellationToken cancellationToken)
    {
        bool locationExists = await reservationQueryService.LocationExistsAsync(request.PickupLocationId, cancellationToken);

        if (!locationExists)
            return Result<IReadOnlyList<GetAvailableCarsQueryResponse>>.Failure(
                Error.NotFound("Teslim alma lokasyonu bulunamadı.", nameof(request.PickupLocationId)));

        IReadOnlyList<GetAvailableCarsQueryResponse> cars = await reservationQueryService.GetAvailableCarsAsync(
            request.PickupLocationId,
            request.StartDate,
            request.EndDate,
            cancellationToken);

        return Result<IReadOnlyList<GetAvailableCarsQueryResponse>>.Success(cars);
    }
}
