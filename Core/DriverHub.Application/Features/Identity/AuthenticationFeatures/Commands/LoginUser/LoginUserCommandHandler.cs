using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication.Login;
using DriverHub.Application.Interfaces.Authentication;
using MediatR;

namespace DriverHub.Application.Features.Identity.AuthenticationFeatures.Commands.LoginUser;

public sealed class LoginUserCommandHandler(IAuthenticationService authenticationService) : IRequestHandler<LoginUserCommand, Result<LoginUserCommandResponse>>
{
    public async Task<Result<LoginUserCommandResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        LoginUserRequest loginRequest = new(
            request.Email,
            request.Password);

        Result<LoginUserResponse> loginResult = await authenticationService.LoginAsync(loginRequest, cancellationToken);

        if (loginResult.IsFailure)
            return Result<LoginUserCommandResponse>.Failure(loginResult.Errors);

        LoginUserCommandResponse response = new(
            loginResult.Value.AccessToken,
            loginResult.Value.AccessTokenExpiresAt,
            loginResult.Value.RefreshToken,
            loginResult.Value.RefreshTokenExpiresAt);

        return Result<LoginUserCommandResponse>.Success(response);
    }
}