using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Identity.AccountFeatures.Command.RegisterUser;
using DriverHub.WebApi.Controllers.Abstraction;
using DriverHub.WebApi.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers
{
    public class AccountsController(IMediator mediator) : BaseController(mediator)
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<RegisterUserCommandResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAsync(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            Result<RegisterUserCommandResponse> result = await _mediator.Send(request, cancellationToken);
            return ToActionResult(result, StatusCodes.Status201Created);
        }
    }
}