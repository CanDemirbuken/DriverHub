using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Cars.Commands.ChangeCarLocation;

public sealed record ChangeCarLocationCommand(
    Guid Id,
    Guid CurrentLocationId
) : IRequest<Result>;