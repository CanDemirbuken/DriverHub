using DriverHub.Application.Common.Results;
using DriverHub.Domain.Enums;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Commands.ChangeCarStatus;

public sealed record ChangeCarStatusCommand(
    Guid Id,
    CarStatus Status
) : IRequest<Result>;