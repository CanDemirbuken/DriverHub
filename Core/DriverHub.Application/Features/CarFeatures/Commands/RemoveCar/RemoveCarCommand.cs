using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.CarFeatures.Commands.RemoveCar;

public sealed record RemoveCarCommand(Guid Id) : IRequest<Result>;