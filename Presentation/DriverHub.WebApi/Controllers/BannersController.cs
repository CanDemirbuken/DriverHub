using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Entities.BannerFeatures.Commands.CreateBanner;
using DriverHub.Application.Features.Entities.BannerFeatures.Commands.RemoveBanner;
using DriverHub.Application.Features.Entities.BannerFeatures.Commands.UpdateBanner;
using DriverHub.Application.Features.Entities.BannerFeatures.Queries.GetAllBanner;
using DriverHub.Application.Features.Entities.BannerFeatures.Queries.GetBannerById;
using DriverHub.WebApi.Controllers.Abstraction;
using DriverHub.WebApi.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers;

public sealed class BannersController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<GetAllBannerQueryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        Result<IReadOnlyList<GetAllBannerQueryResponse>> result = await _mediator.Send(new GetAllBannerQuery(), cancellationToken);
        return ToActionResult(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<GetBannerByIdQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Result<GetBannerByIdQueryResponse> result = await _mediator.Send(new GetBannerByIdQuery(id), cancellationToken);
        return ToActionResult(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateBannerCommandResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        Result<CreateBannerCommandResponse> result = await _mediator.Send(request, cancellationToken);
        return ToActionResult(result, StatusCodes.Status201Created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateBannerCommand request, CancellationToken cancellationToken)
    {
        UpdateBannerCommand command = request with
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
        Result result = await _mediator.Send(new RemoveBannerCommand(id), cancellationToken);
        return ToActionResult(result, StatusCodes.Status204NoContent);
    }
}