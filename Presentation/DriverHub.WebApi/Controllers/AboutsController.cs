using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.AboutFeatures.Commands.CreateAbout;
using DriverHub.Application.Features.AboutFeatures.Commands.RemoveAbout;
using DriverHub.Application.Features.AboutFeatures.Commands.UpdateAbout;
using DriverHub.Application.Features.AboutFeatures.Queries.GetAboutById;
using DriverHub.Application.Features.AboutFeatures.Queries.GetAllAbout;
using DriverHub.WebApi.Controllers.Abstraction;
using DriverHub.WebApi.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers;

public sealed class AboutsController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<GetAllAboutQueryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        Result<IReadOnlyList<GetAllAboutQueryResponse>> result = await _mediator.Send(new GetAllAboutQuery(), cancellationToken);
        return ToActionResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<GetAboutByIdQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<GetAboutByIdQueryResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Result<GetAboutByIdQueryResponse> result = await _mediator.Send(new GetAboutByIdQuery(id), cancellationToken);
        return ToActionResult(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateAboutCommandResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<CreateAboutCommandResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync(CreateAboutCommand request, CancellationToken cancellationToken)
    {
        Result<CreateAboutCommandResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateAboutCommand request, CancellationToken cancellationToken)
    {
        UpdateAboutCommand command = request with
        {
            Id = id
        };

        Result result = await _mediator.Send(command, cancellationToken);
        return ToActionResult(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        Result result = await _mediator.Send(new RemoveAboutCommand(id), cancellationToken);
        return ToActionResult(result);
    }
}