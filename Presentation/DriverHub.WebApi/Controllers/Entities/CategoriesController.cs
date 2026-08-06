using DriverHub.Application.Common.Constants;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.CategoryFeatures.Commands.CreateCategory;
using DriverHub.Application.Features.Entities.CategoryFeatures.Commands.RemoveCategory;
using DriverHub.Application.Features.Entities.CategoryFeatures.Commands.UpdateCategory;
using DriverHub.Application.Features.Entities.CategoryFeatures.Queries.GetAllCategory;
using DriverHub.Application.Features.Entities.CategoryFeatures.Queries.GetCategoryById;
using DriverHub.WebApi.Common.API;
using DriverHub.WebApi.Controllers.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers.Entities;

public sealed class CategoriesController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<GetAllCategoryQueryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        Result<IReadOnlyList<GetAllCategoryQueryResponse>> result = await _mediator.Send(new GetAllCategoryQuery(), cancellationToken);
        return ToActionResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<GetCategoryByIdQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Result<GetCategoryByIdQueryResponse> result = await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
        return ToActionResult(result);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateCategoryCommandResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAsync(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Result<CreateCategoryCommandResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result, StatusCodes.Status201Created);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        UpdateCategoryCommand command = request with
        {
            Id = id
        };

        Result result = await _mediator.Send(command, cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        Result result = await _mediator.Send(new RemoveCategoryCommand(id), cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }
}