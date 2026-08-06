using DriverHub.Application.Common.Constants;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Identity.RoleFeatures.Commands.CreateRole;
using DriverHub.Application.Features.Identity.RoleFeatures.Commands.RemoveRole;
using DriverHub.Application.Features.Identity.RoleFeatures.Commands.UpdateRole;
using DriverHub.Application.Features.Identity.RoleFeatures.Queries.GetAllRoles;
using DriverHub.Application.Features.Identity.RoleFeatures.Queries.GetRoleById;
using DriverHub.WebApi.Common.API;
using DriverHub.WebApi.Controllers.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers.Identity;

public sealed class RolesController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<GetAllRolesQueryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        Result<IReadOnlyList<GetAllRolesQueryResponse>> result = await _mediator.Send(new GetAllRolesQuery(), cancellationToken);
        return ToActionResult(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<GetRoleByIdQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        Result<GetRoleByIdQueryResponse> result = await _mediator.Send(new GetRoleByIdQuery(id), cancellationToken);
        return ToActionResult(result);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateRoleCommandResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAsync(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        Result<CreateRoleCommandResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result, StatusCodes.Status201Created);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateAsync(string id, UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;

        Result result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RemoveAsync(string id, CancellationToken cancellationToken)
    {
        Result result = await _mediator.Send(new RemoveRoleCommand(id), cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }
}