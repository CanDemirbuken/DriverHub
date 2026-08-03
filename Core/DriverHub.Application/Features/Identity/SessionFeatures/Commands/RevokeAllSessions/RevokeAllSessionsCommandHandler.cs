using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Authentication;
using MediatR;

namespace DriverHub.Application.Features.Identity.SessionFeatures.Commands.RevokeAllSessions;

public sealed class RevokeAllSessionsCommandHandler(ISessionService sessionService) : IRequestHandler<RevokeAllSessionsCommand, Result>
{
    public async Task<Result> Handle(RevokeAllSessionsCommand request, CancellationToken cancellationToken)
    {
        return await sessionService.RevokeAllAsync(request.UserId, cancellationToken);
    }
}