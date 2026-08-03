using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Authentication;
using MediatR;

namespace DriverHub.Application.Features.Identity.SessionFeatures.Commands.RevokeSession;

public sealed class RevokeSessionCommandHandler(ISessionService sessionService) : IRequestHandler<RevokeSessionCommand, Result>
{
    public Task<Result> Handle(RevokeSessionCommand request, CancellationToken cancellationToken)
    {
        return sessionService.RevokeAsync(request.RefreshToken, cancellationToken);
    }
}