using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication.Token.RefreshToken;
using DriverHub.Application.Features.Identity.AccountFeatures.Command.RegisterUser;
using DriverHub.Application.Features.Identity.AuthenticationFeatures.Commands.LoginUser;
using DriverHub.Application.Features.Identity.SessionFeatures.Commands.RefreshToken;
using DriverHub.WebApi.Controllers.Abstraction;
using DriverHub.WebApi.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers;

[AllowAnonymous]
public sealed class AuthenticationController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<LoginUserCommandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status423Locked)]
    public async Task<IActionResult> LoginAsync(LoginUserCommand request, CancellationToken cancellationToken)
    {
        Result<LoginUserCommandResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result);
    }
}