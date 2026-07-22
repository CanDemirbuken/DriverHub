using MediatR;

namespace DriverHub.Application.Features.AboutFeatures.Commands.RemoveAbout;

public sealed record RemoveAboutCommand(Guid Id) : IRequest;