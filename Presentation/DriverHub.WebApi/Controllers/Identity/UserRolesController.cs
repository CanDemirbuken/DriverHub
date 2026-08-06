using DriverHub.Application.Common.Constants;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Identity.UserRoleFeatures.Commands.AssignRoleToUser;
using DriverHub.Application.Features.Identity.UserRoleFeatures.Commands.RemoveRoleFromUser;
using DriverHub.WebApi.Common.API;
using DriverHub.WebApi.Controllers.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers.Identity;

[Authorize(Policy = AuthorizationPolicies.AdminOnly)]
[Route("api/users/{userId}/roles")]
public sealed class UserRolesController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost("{roleId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AssignRoleAsync(string userId, string roleId, CancellationToken cancellationToken)
    {
        Result result = await _mediator.Send(new AssignRoleToUserCommand(userId, roleId), cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }

    [HttpDelete("{roleId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RemoveRoleAsync(string userId, string roleId, CancellationToken cancellationToken)
    {
        Result result = await _mediator.Send(new RemoveRoleFromUserCommand(userId, roleId), cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }
}