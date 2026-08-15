using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Locations.Commands.RemoveLocation;

public sealed record RemoveLocationCommand(
    Guid Id
) : IRequest<Result>;