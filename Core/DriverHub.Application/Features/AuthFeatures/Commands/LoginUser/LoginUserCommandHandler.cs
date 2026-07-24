using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Login;
using DriverHub.Application.Interfaces.Identity;
using MediatR;

namespace DriverHub.Application.Features.AuthFeatures.Commands.LoginUser;

public sealed class LoginUserCommandHandler(IIdentityService identityService) : IRequestHandler<LoginUserCommand, Result<LoginUserCommandResponse>>
{
    public async Task<Result<LoginUserCommandResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        LoginUserRequest loginRequest = new(request.Email, request.Password);

        Result<LoginUserResponse> loginResult = await identityService.LoginAsync(loginRequest, cancellationToken);

        if (loginResult.IsFailure)
            return Result<LoginUserCommandResponse>.Failure(loginResult.Errors);

        LoginUserCommandResponse response = new(
            loginResult.Value.AccessToken,
            loginResult.Value.AccessTokenExpiresAt);

        return Result<LoginUserCommandResponse>.Success(response);
    }
}