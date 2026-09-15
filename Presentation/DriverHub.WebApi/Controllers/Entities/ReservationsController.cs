using System.Security.Claims;
using DriverHub.Application.Common.Constants;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.Reservations.Commands.CreateReservation;
using DriverHub.Application.Features.Entities.Reservations.Queries.GetAvailableCars;
using DriverHub.Application.Features.Entities.Reservations.Queries.GetReservationQuote;
using DriverHub.WebApi.Common.API;
using DriverHub.WebApi.Contracts.Reservations;
using DriverHub.WebApi.Controllers.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DriverHub.Application.Common.Models;
using DriverHub.Application.Features.Entities.Reservations.Common;
using DriverHub.Application.Features.Entities.Reservations.Queries.GetPagedReservations;
using DriverHub.Application.Features.Entities.Reservations.Queries.GetReservationById;
using DriverHub.Application.Features.Entities.Reservations.Commands.UpdateReservationStatus;
using DriverHub.Domain.Enums;

namespace DriverHub.WebApi.Controllers.Entities;

public sealed class ReservationsController(IMediator mediator) : BaseController(mediator)
{
    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ReservationResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPagedAsync([FromQuery] GetPagedReservationsQuery request, CancellationToken cancellationToken) =>
        ToActionResult(await _mediator.Send(request, cancellationToken));

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ReservationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        ToActionResult(await _mediator.Send(new GetReservationByIdQuery(id), cancellationToken));

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpPost("{id:guid}/approve")]
    public Task<IActionResult> ApproveAsync(Guid id, CancellationToken cancellationToken) =>
        UpdateStatusAsync(id, ReservationStatus.Confirmed, cancellationToken);

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpPost("{id:guid}/cancel")]
    public Task<IActionResult> CancelAsync(Guid id, CancellationToken cancellationToken) =>
        UpdateStatusAsync(id, ReservationStatus.Cancelled, cancellationToken);

    private async Task<IActionResult> UpdateStatusAsync(Guid id, ReservationStatus status, CancellationToken cancellationToken)
    {
        string? actorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(actorId)) return Unauthorized();
        return ToActionResult(await _mediator.Send(new UpdateReservationStatusCommand(id, status, actorId), cancellationToken));
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpGet("availability")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<GetAvailableCarsQueryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAvailabilityAsync(
        [FromQuery] GetAvailableCarsQuery request,
        CancellationToken cancellationToken)
    {
        Result<IReadOnlyList<GetAvailableCarsQueryResponse>> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpGet("quote")]
    [ProducesResponseType(typeof(ApiResponse<GetReservationQuoteResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetQuoteAsync([FromQuery] GetReservationQuoteQuery request, CancellationToken cancellationToken)
    {
        Result<GetReservationQuoteResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateReservationCommandResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAsync(
        CreateReservationRequest request,
        CancellationToken cancellationToken)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized();

        Result<CreateReservationCommandResponse> result = await _mediator.Send(
            new CreateReservationCommand(
                userId,
                request.CarId,
            request.PickupLocationId,
            request.StartDate,
            request.EndDate,
            request.ExtraIds ?? [],
            request.InsurancePackageId),
            cancellationToken);

        return ToActionResult(result, StatusCodes.Status201Created);
    }

}
