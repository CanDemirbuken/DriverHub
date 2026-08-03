using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.FeatureFeatures.Commands.CreateFeature;
using DriverHub.Application.Features.Entities.FeatureFeatures.Commands.RemoveFeature;
using DriverHub.Application.Features.Entities.FeatureFeatures.Commands.UpdateFeature;
using DriverHub.Application.Features.Entities.FeatureFeatures.Queries.GetAllFeature;
using DriverHub.Application.Features.Entities.FeatureFeatures.Queries.GetFeatureById;
using DriverHub.WebApi.Controllers.Abstraction;
using DriverHub.WebApi.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers;

public sealed class FeaturesController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<GetAllFeatureQueryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        Result<IReadOnlyList<GetAllFeatureQueryResponse>> result = await _mediator.Send(new GetAllFeatureQuery(), cancellationToken);
        return ToActionResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<GetFeatureByIdQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Result<GetFeatureByIdQueryResponse> result = await _mediator.Send(new GetFeatureByIdQuery(id), cancellationToken);
        return ToActionResult(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateFeatureCommandResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAsync(CreateFeatureCommand request, CancellationToken cancellationToken)
    {
        Result<CreateFeatureCommandResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result, StatusCodes.Status201Created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateFeatureCommand request, CancellationToken cancellationToken)
    {
        UpdateFeatureCommand command = request with
        {
            Id = id
        };

        Result result = await _mediator.Send(command, cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        Result result = await _mediator.Send(new RemoveFeatureCommand(id), cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }
}