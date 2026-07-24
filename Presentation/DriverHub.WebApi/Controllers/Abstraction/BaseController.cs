using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers.Abstraction;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator _mediator = mediator;

    protected IActionResult CreatedAtId<TResponse>(string actionName, Guid id, TResponse response)
    {
        return CreatedAtAction(
            actionName,
            new { id },
            response);
    }
}