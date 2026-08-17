using DriverHub.Application.Common.Constants;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Features.Media.Commands;
using DriverHub.WebApi.Contracts.Media;
using DriverHub.WebApi.Controllers.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers
{
    public class MediaController(IMediator mediator) : BaseController(mediator)
    {
        [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadMediaRequest request, CancellationToken cancellationToken)
        {
            IFormFile file = request.File;

            UploadMediaCommand command = new UploadMediaCommand(
                file.FileName,
                file.ContentType,
                file.Length,
                file.OpenReadStream()
            );

            Result<UploadMediaCommandResponse> result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
