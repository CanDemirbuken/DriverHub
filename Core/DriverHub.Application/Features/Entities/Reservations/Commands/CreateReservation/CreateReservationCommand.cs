using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Reservations.Commands.CreateReservation;

public sealed record CreateReservationCommand(
    string UserId,
    Guid CarId,
    Guid PickupLocationId,
    DateTime StartDate,
    DateTime EndDate,
    IReadOnlyCollection<Guid> ExtraIds,
    Guid? InsurancePackageId) : IRequest<Result<CreateReservationCommandResponse>>;
