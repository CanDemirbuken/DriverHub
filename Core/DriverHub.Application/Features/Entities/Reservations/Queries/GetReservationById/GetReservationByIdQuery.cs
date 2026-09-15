using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.Reservations.Common;
using MediatR;

namespace DriverHub.Application.Features.Entities.Reservations.Queries.GetReservationById;

public sealed record GetReservationByIdQuery(Guid Id) : IRequest<Result<ReservationResponse>>;
