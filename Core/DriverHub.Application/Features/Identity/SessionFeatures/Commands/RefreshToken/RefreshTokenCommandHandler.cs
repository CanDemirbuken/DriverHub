using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Token.RefreshToken;
using DriverHub.Application.Interfaces.Authentication;
using MediatR;

namespace DriverHub.Application.Features.Identity.SessionFeatures.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler(ISessionService sessionService) : IRequestHandler<RefreshTokenCommand, Result<RefreshSessionResponse>>
{
    public async Task<Result<RefreshSessionResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await sessionService.RefreshTokenAsync(request.RefreshToken, cancellationToken);
    }
}