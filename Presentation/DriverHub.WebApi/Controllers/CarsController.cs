using DriverHub.Application.Common.Models;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.CarFeatures.Commands.CreateCar;
using DriverHub.Application.Features.CarFeatures.Commands.RemoveCar;
using DriverHub.Application.Features.CarFeatures.Commands.UpdateCar;
using DriverHub.Application.Features.CarFeatures.Queries.GetCarByIdWithBrand;
using DriverHub.Application.Features.CarFeatures.Queries.GetPagedCarsWithBrand;
using DriverHub.WebApi.Controllers.Abstraction;
using DriverHub.WebApi.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers;

public sealed class CarsController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<GetPagedCarsWithBrandQueryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<GetPagedCarsWithBrandQueryResponse>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPagedAsync([FromQuery] GetPagedCarsWithBrandQuery request, CancellationToken cancellationToken)
    {
        Result<PagedResponse<GetPagedCarsWithBrandQueryResponse>> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<GetCarByIdWithBrandQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<GetCarByIdWithBrandQueryResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Result<GetCarByIdWithBrandQueryResponse> result = await _mediator.Send(new GetCarByIdWithBrandQuery(id), cancellationToken);
        return ToActionResult(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateCarCommandResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<CreateCarCommandResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CreateCarCommandResponse>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<CreateCarCommandResponse>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAsync(CreateCarCommand request, CancellationToken cancellationToken)
    {
        Result<CreateCarCommandResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateCarCommand request, CancellationToken cancellationToken)
    {
        UpdateCarCommand command = request with
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
        Result result = await _mediator.Send(new RemoveCarCommand(id), cancellationToken);
        return ToActionResult(result);
    }
}