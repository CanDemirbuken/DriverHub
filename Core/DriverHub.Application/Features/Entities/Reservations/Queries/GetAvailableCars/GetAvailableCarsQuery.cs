using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Reservations.Queries.GetAvailableCars;

public sealed record GetAvailableCarsQuery(
    Guid PickupLocationId,
    DateTime StartDate,
    DateTime EndDate) : IRequest<Result<IReadOnlyList<GetAvailableCarsQueryResponse>>>;
