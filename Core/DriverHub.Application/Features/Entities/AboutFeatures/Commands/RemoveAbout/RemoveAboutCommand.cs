using DriverHub.Application.Common.Results;
using MediatR;

namespace DriverHub.Application.Features.Entities.AboutFeatures.Commands.RemoveAbout;

public sealed record RemoveAboutCommand(Guid Id) : IRequest<Result>;