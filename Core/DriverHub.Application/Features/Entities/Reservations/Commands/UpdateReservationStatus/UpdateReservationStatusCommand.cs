using DriverHub.Application.Common.Results;
using DriverHub.Domain.Enums;
using MediatR;

namespace DriverHub.Application.Features.Entities.Reservations.Commands.UpdateReservationStatus;

// Actor and target status come from the controller, never from a request body.
public sealed record UpdateReservationStatusCommand(Guid Id, ReservationStatus Status, string ActorId) : IRequest<Result>;
