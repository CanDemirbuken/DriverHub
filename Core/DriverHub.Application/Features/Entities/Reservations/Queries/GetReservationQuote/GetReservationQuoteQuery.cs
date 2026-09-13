using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Reservations.Queries.GetReservationQuote;

public sealed record GetReservationQuoteQuery(
    Guid CarId,
    Guid PickupLocationId,
    DateTime StartDate,
    DateTime EndDate,
    IReadOnlyCollection<Guid>? ExtraIds,
    Guid? InsurancePackageId) : IRequest<Result<GetReservationQuoteResponse>>;
