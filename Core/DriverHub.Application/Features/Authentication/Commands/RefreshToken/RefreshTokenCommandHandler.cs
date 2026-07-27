using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication.Token.RefreshToken;
using DriverHub.Application.Interfaces.Authentication;
using MediatR;

namespace DriverHub.Application.Features.Authentication.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler(IAuthenticationService authenticationService) : IRequestHandler<RefreshTokenCommand, Result<RefreshSessionResponse>>
{
    public async Task<Result<RefreshSessionResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await authenticationService.RefreshTokenAsync(request.RefreshToken, cancellationToken);

    }
}
