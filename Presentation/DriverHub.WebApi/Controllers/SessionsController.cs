using System.Security.Claims;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Token.RefreshToken;
using DriverHub.Application.Features.Identity.SessionFeatures.Commands.RefreshToken;
using DriverHub.Application.Features.Identity.SessionFeatures.Commands.RevokeAllSessions;
using DriverHub.Application.Features.Identity.SessionFeatures.Commands.RevokeSession;
using DriverHub.WebApi.Controllers.Abstraction;
using DriverHub.WebApi.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers;

public sealed class SessionsController(IMediator mediator) : BaseController(mediator)
{
    [AllowAnonymous]
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ApiResponse<RefreshSessionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        Result<RefreshSessionResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    [AllowAnonymous]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LogoutAsync(RevokeSessionCommand request, CancellationToken cancellationToken)
    {
        Result result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }

    [HttpPost("logout-all")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LogoutAllAsync(CancellationToken cancellationToken)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized();

        Result result = await _mediator.Send(new RevokeAllSessionsCommand(userId), cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }
}