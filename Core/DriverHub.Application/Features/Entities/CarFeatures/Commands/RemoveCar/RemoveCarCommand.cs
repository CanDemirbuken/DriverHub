using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.CarFeatures.Commands.RemoveCar;

public sealed record RemoveCarCommand(Guid Id) : IRequest<Result>;