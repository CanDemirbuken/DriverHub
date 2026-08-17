using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Identity.AuthenticationFeatures.Commands.LoginUser;
using DriverHub.WebApi.Common.API;
using DriverHub.WebApi.Common.Cookies;
using DriverHub.WebApi.Common.RateLimiting;
using DriverHub.WebApi.Contracts.Identity.Authentication;
using DriverHub.WebApi.Controllers.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DriverHub.WebApi.Controllers.Identity;

public sealed class AuthenticationController(IMediator mediator, RefreshTokenCookieManager refreshTokenCookieManager) : BaseController(mediator)
{
    private readonly RefreshTokenCookieManager _refreshTokenCookieManager =
        refreshTokenCookieManager;

    [AllowAnonymous]
    [EnableRateLimiting(RateLimitPolicyNames.Login)]
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<AuthenticationResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status423Locked)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> LoginAsync(LoginUserCommand request, CancellationToken cancellationToken)
    {
        Result<LoginUserCommandResponse> result = await _mediator.Send(request, cancellationToken);

        if (result.IsFailure)
            return ToActionResult(Result<AuthenticationResponse>.Failure(result.Errors));


        LoginUserCommandResponse session = result.Value;

        _refreshTokenCookieManager.Append(
            Response,
            session.RefreshToken,
            session.RefreshTokenExpiresAt);

        var response = new AuthenticationResponse(
            session.AccessToken,
            session.AccessTokenExpiresAt);

        return ToActionResult(Result<AuthenticationResponse>.Success(response));
    }
}