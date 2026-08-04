using DriverHub.Application.Common.Errors.Identity;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Authentication.Login;
using DriverHub.Application.Contracts.Identity.Session;
using DriverHub.Application.Interfaces.Authentication;
using DriverHub.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace DriverHub.Infrastructure.Services.Identity.Authentication;

public sealed class AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ISessionService sessionService) : IAuthenticationService
{
    public async Task<Result<LoginUserResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken = default)
    {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);

        if (user is null || !user.IsActive || user.IsDeleted)
            return Result<LoginUserResponse>.Failure(AuthenticationErrors.InvalidCredentials);

        SignInResult loginResult = await signInManager.CheckPasswordSignInAsync(
            user,
            request.Password,
            lockoutOnFailure: true);

        if (loginResult.IsLockedOut)
            return Result<LoginUserResponse>.Failure(AuthenticationErrors.UserLocked);

        if (!loginResult.Succeeded)
            return Result<LoginUserResponse>.Failure(AuthenticationErrors.InvalidCredentials);

        if (!user.EmailConfirmed)
            return Result<LoginUserResponse>.Failure(AuthenticationErrors.EmailNotConfirmed);

        IList<string> roles = await userManager.GetRolesAsync(user);

        var sessionRequest = new CreateSessionRequest(
            user.Id,
            user.Email!,
            roles.ToArray());

        Result<SessionResponse> sessionResult = await sessionService.CreateSessionAsync(sessionRequest, cancellationToken);

        if (sessionResult.IsFailure)
            return Result<LoginUserResponse>.Failure(sessionResult.Errors);

        SessionResponse session = sessionResult.Value;

        var response = new LoginUserResponse(
            session.AccessToken,
            session.AccessTokenExpiresAt,
            session.RefreshToken,
            session.RefreshTokenExpiresAt);

        return Result<LoginUserResponse>.Success(response);
    }
}