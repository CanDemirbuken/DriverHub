using DriverHub.Application.Common.Models;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.ContactFeatures.Commands.CreateContact;
using DriverHub.Application.Features.Entities.ContactFeatures.Commands.RemoveContact;
using DriverHub.Application.Features.Entities.ContactFeatures.Commands.UpdateContact;
using DriverHub.Application.Features.Entities.ContactFeatures.Queries.GetContactById;
using DriverHub.Application.Features.Entities.ContactFeatures.Queries.GetPagedContacts;
using DriverHub.WebApi.Controllers.Abstraction;
using DriverHub.WebApi.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers;

public sealed class ContactsController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<GetPagedContactsQueryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPagedAsync([FromQuery] GetPagedContactsQuery request, CancellationToken cancellationToken)
    {
        Result<PagedResponse<GetPagedContactsQueryResponse>> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<GetContactByIdQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Result<GetContactByIdQueryResponse> result = await _mediator.Send(new GetContactByIdQuery(id), cancellationToken);
        return ToActionResult(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateContactCommandResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync(CreateContactCommand request, CancellationToken cancellationToken)
    {
        Result<CreateContactCommandResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result, StatusCodes.Status201Created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateContactCommand request, CancellationToken cancellationToken)
    {
        UpdateContactCommand command = request with
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
        Result result = await _mediator.Send(new RemoveContactCommand(id), cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }
}