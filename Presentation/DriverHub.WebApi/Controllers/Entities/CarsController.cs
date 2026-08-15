using DriverHub.Application.Common.Constants;
using DriverHub.Application.Common.Models;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.Cars.Commands.ChangeCarLocation;
using DriverHub.Application.Features.Entities.Cars.Commands.ChangeCarStatus;
using DriverHub.Application.Features.Entities.Cars.Commands.CreateCar;
using DriverHub.Application.Features.Entities.Cars.Commands.SetCarFeatures;
using DriverHub.Application.Features.Entities.Cars.Commands.SetCarPricings;
using DriverHub.Application.Features.Entities.Cars.Commands.UpdateCar;
using DriverHub.Application.Features.Entities.Cars.Queries.GetCarById;
using DriverHub.Application.Features.Entities.Cars.Queries.GetPagedCars;
using DriverHub.WebApi.Common.API;
using DriverHub.WebApi.Contracts.Cars;
using DriverHub.WebApi.Controllers.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers.Entities;

public sealed class CarsController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<GetPagedCarsQueryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPagedAsync([FromQuery] GetPagedCarsQuery request, CancellationToken cancellationToken)
    {
        Result<PagedResponse<GetPagedCarsQueryResponse>> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<GetCarByIdQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Result<GetCarByIdQueryResponse> result = await _mediator.Send(new GetCarByIdQuery(id), cancellationToken);
        return ToActionResult(result);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateCarCommandResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAsync(CreateCarCommand request, CancellationToken cancellationToken)
    {
        Result<CreateCarCommandResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result, StatusCodes.Status201Created);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
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
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeStatusAsync(Guid id, ChangeCarStatusRequest request, CancellationToken cancellationToken)
    {
        Result result = await _mediator.Send(new ChangeCarStatusCommand(id, request.Status), cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpPatch("{id:guid}/location")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeLocationAsync(Guid id, ChangeCarLocationRequest request, CancellationToken cancellationToken)
    {
        Result result = await _mediator.Send(new ChangeCarLocationCommand(id, request.CurrentLocationId), cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpPut("{id:guid}/features")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetFeaturesAsync(Guid id, SetCarFeaturesRequest request, CancellationToken cancellationToken)
    {
        Result result = await _mediator.Send(new SetCarFeaturesCommand(id, request.FeatureIds), cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpPut("{id:guid}/pricings")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetPricingsAsync(Guid id, SetCarPricingsRequest request, CancellationToken cancellationToken)
    {
        SetCarPricingItem[] pricings = request.Pricings
            .Select(pricing => new SetCarPricingItem(
                pricing.Type,
                pricing.Amount))
            .ToArray();

        Result result = await _mediator.Send(new SetCarPricingsCommand(id, pricings), cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }
}