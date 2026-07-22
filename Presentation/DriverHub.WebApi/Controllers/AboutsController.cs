using DriverHub.Application.Features.AboutFeatures.Commands.CreateAbout;
using DriverHub.Application.Features.AboutFeatures.Commands.RemoveAbout;
using DriverHub.Application.Features.AboutFeatures.Commands.UpdateAbout;
using DriverHub.Application.Features.AboutFeatures.Queries.GetAboutById;
using DriverHub.Application.Features.AboutFeatures.Queries.GetAllAbout;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public sealed class AboutsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllAboutQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAboutByIdQuery(id), cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAboutCommand request, CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);
        return Ok("Ekleme işlemi başarılı");
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAboutCommand request, CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);
        return Ok("Güncelleme işlemi başarılı");
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new RemoveAboutCommand(id), cancellationToken);
        return Ok("Silme işlemi başarılı");
    }
}