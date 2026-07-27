using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication.Token.RefreshToken;
using DriverHub.Application.Features.Authentication.Commands.LoginUser;
using DriverHub.Application.Features.Authentication.Commands.RefreshToken;
using DriverHub.Application.Features.Authentication.Commands.RegisterUser;
using DriverHub.WebApi.Controllers.Abstraction;
using DriverHub.WebApi.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers;

[AllowAnonymous]
public sealed class AuthenticationController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponse<RegisterUserCommandResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<RegisterUserCommandResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<RegisterUserCommandResponse>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterAsync(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        Result<RegisterUserCommandResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<LoginUserCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<LoginUserCommandResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<LoginUserCommandResponse>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<LoginUserCommandResponse>), StatusCodes.Status423Locked)]
    public async Task<IActionResult> LoginAsync(LoginUserCommand request, CancellationToken cancellationToken)
    {
        Result<LoginUserCommandResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ApiResponse<RefreshSessionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<RefreshSessionResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<RefreshSessionResponse>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        Result<RefreshSessionResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result);
    }
}