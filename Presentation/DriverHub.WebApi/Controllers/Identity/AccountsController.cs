using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Identity.AccountFeatures.Command.ConfirmEmail;
using DriverHub.Application.Features.Identity.AccountFeatures.Command.ForgotPassword;
using DriverHub.Application.Features.Identity.AccountFeatures.Command.RegisterUser;
using DriverHub.Application.Features.Identity.AccountFeatures.Command.ResetPassword;
using DriverHub.WebApi.Common.API;
using DriverHub.WebApi.Common.RateLimiting;
using DriverHub.WebApi.Controllers.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DriverHub.WebApi.Controllers.Identity
{
    public class AccountsController(IMediator mediator) : BaseController(mediator)
    {
        [AllowAnonymous]
        [EnableRateLimiting(RateLimitPolicyNames.Registration)]
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<RegisterUserCommandResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> RegisterAsync(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            Result<RegisterUserCommandResponse> result = await _mediator.Send(request, cancellationToken);
            return ToActionResult(result, StatusCodes.Status201Created);
        }

        [AllowAnonymous]
        [EnableRateLimiting(RateLimitPolicyNames.ConfirmEmail)]
        [HttpPost("confirm-email")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> ConfirmEmailAsync(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            Result result = await _mediator.Send(request, cancellationToken);
            return ToActionResult(result);
        }

        [AllowAnonymous]
        [EnableRateLimiting(RateLimitPolicyNames.ForgotPassword)]
        [HttpPost("forgot-password")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            Result result = await _mediator.Send(request, cancellationToken);
            return ToActionResult(result);
        }

        [AllowAnonymous]
        [EnableRateLimiting(RateLimitPolicyNames.ResetPassword)]
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            Result result = await _mediator.Send(request, cancellationToken);
            return ToActionResult(result);
        }
    }
}