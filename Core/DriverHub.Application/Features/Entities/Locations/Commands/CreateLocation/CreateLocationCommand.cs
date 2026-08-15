using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.Locations.Commands.CreateLocation;

public sealed record CreateLocationCommand(string Name) : IRequest<Result<CreateLocationCommandResponse>>;