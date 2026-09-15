using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.Reservations.Common;
using DriverHub.Application.Interfaces.QueryServices;
using MediatR;

namespace DriverHub.Application.Features.Entities.Reservations.Queries.GetReservationById;

public sealed class GetReservationByIdQueryHandler(IReservationQueryService queryService)
    : IRequestHandler<GetReservationByIdQuery, Result<ReservationResponse>>
{
    public async Task<Result<ReservationResponse>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        ReservationResponse? reservation = await queryService.GetByIdAsync(request.Id, cancellationToken);
        return reservation is null
            ? Result<ReservationResponse>.Failure(Error.NotFound("Rezervasyon bulunamadı."))
            : Result<ReservationResponse>.Success(reservation);
    }
}
