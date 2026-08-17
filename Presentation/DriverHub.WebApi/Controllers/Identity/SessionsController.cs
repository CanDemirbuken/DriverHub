using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Token.RefreshToken;
using DriverHub.Application.Features.Identity.SessionFeatures.Commands.RefreshToken;
using DriverHub.Application.Features.Identity.SessionFeatures.Commands.RevokeAllSessions;
using DriverHub.Application.Features.Identity.SessionFeatures.Commands.RevokeSession;
using DriverHub.WebApi.Common.API;
using DriverHub.WebApi.Common.Cookies;
using DriverHub.WebApi.Common.RateLimiting;
using DriverHub.WebApi.Contracts.Identity.Session;
using DriverHub.WebApi.Controllers.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace DriverHub.WebApi.Controllers.Identity;

public sealed class SessionsController(IMediator mediator, RefreshTokenCookieManager refreshTokenCookieManager) : BaseController(mediator)
{
    private readonly RefreshTokenCookieManager _refreshTokenCookieManager = refreshTokenCookieManager;

    [AllowAnonymous]
    [EnableRateLimiting(RateLimitPolicyNames.RefreshToken)]
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ApiResponse<RefreshSessionHttpResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status429TooManyRequests)]
    public async Task<IActionResult> RefreshTokenAsync(CancellationToken cancellationToken)
    {
        string? refreshToken = _refreshTokenCookieManager.Get(Request);

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return ToActionResult(
                Result<RefreshSessionHttpResponse>.Failure(
                    Error.Unauthorized(
                        "Refresh token bulunamadı.")));
        }

        var command = new RefreshTokenCommand(refreshToken);

        Result<RefreshSessionResponse> result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            _refreshTokenCookieManager.Delete(Response);

            return ToActionResult(Result<RefreshSessionHttpResponse>.Failure(result.Errors));
        }

        RefreshSessionResponse session = result.Value;

        _refreshTokenCookieManager.Append(
            Response,
            session.RefreshToken,
            session.RefreshTokenExpiresAt);

        var response = new RefreshSessionHttpResponse(
                session.AccessToken,
                session.AccessTokenExpiresAt);

        return ToActionResult(Result<RefreshSessionHttpResponse>.Success(response));
    }

    [AllowAnonymous]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LogoutAsync(CancellationToken cancellationToken)
    {
        string? refreshToken = _refreshTokenCookieManager.Get(Request);

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            _refreshTokenCookieManager.Delete(Response);
            return ToActionResult(Result.Failure(Error.Unauthorized("Aktif oturum bulunamadı.")));
        }

        var command = new RevokeSessionCommand(refreshToken);
        Result result = await _mediator.Send(command, cancellationToken);

        /*
         * Cookie'yi result ne olursa olsun siliyoruz.
         *
         * Çünkü client tarafında logout talebi verildiyse,
         * browser'ın artık bu session cookie'sini taşımaması gerekir.
         */
        _refreshTokenCookieManager.Delete(Response);

        return ToActionResult(result, StatusCodes.Status204NoContent);
    }

    [HttpPost("logout-all")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LogoutAllAsync(CancellationToken cancellationToken)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
            return ToActionResult(Result.Failure(Error.Unauthorized("Kullanıcı bilgisi bulunamadı.")));


        Result result = await _mediator.Send(new RevokeAllSessionsCommand(userId), cancellationToken);

        if (result.IsSuccess)
            _refreshTokenCookieManager.Delete(Response);

        return ToActionResult(result, StatusCodes.Status204NoContent);
    }
}