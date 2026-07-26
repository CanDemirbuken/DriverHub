using DriverHub.Application.Features.CarFeatures.Commands.CreateCar;
using DriverHub.Application.Features.CarFeatures.Commands.RemoveCar;
using DriverHub.Application.Features.CarFeatures.Queries.GetCarByIdWithBrand;
using DriverHub.Application.Features.CarFeatures.Queries.GetPagedCarsWithBrand;
using DriverHub.WebApi.Controllers.Abstraction;
using DriverHub.WebApi.Mappings;
using DriverHub.WebApi.Models.Cars;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers;

public sealed class CarsController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    [ProducesResponseType(typeof(GetPagedCarsWithBrandQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPagedAsync(
        [FromQuery] GetPagedCarsWithBrandQuery request,
        CancellationToken cancellationToken)
        => Ok(await _mediator.Send(request, cancellationToken));

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetCarByIdWithBrandQueryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetCarByIdWithBrandQuery(id), cancellationToken));

    //[HttpPost]
    //[ProducesResponseType(typeof(CreateCarCommandResponse), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(StatusCodes.Status409Conflict)]
    //public async Task<IActionResult> CreateAsync(
    //    CreateCarCommand request,
    //    CancellationToken cancellationToken)
    //{
    //    CreateCarCommandResponse response = await _mediator.Send(request, cancellationToken);

    //    return CreatedAtId(nameof(GetByIdAsync), response.Id, response);
    //}

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateAsync(
        Guid id,
        UpdateCarRequest request,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(request.ToCommand(id), cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new RemoveCarCommand(id), cancellationToken);

        return NoContent();
    }
}