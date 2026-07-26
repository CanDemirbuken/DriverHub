using DriverHub.Application.Common.Results;
using DriverHub.WebApi.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriverHub.WebApi.Controllers.Abstraction;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator _mediator = mediator;

    protected IActionResult ToActionResult(Result result)
    {
        ApiResponse<object> response = result.IsSuccess
            ? new ApiResponse<object>(
                true,
                null,
                [])
            : new ApiResponse<object>(
                false,
                null,
                MapErrors(result.Errors));

        return StatusCode(
            result.StatusCode,
            response);
    }

    protected IActionResult ToActionResult<TResponse>(
        Result<TResponse> result)
    {
        ApiResponse<TResponse> response = result.IsSuccess
            ? new ApiResponse<TResponse>(
                true,
                result.Value,
                [])
            : new ApiResponse<TResponse>(
                false,
                default,
                MapErrors(result.Errors));

        return StatusCode(
            result.StatusCode,
            response);
    }

    private static IReadOnlyCollection<ApiError> MapErrors(
        IEnumerable<string> errors)
    {
        return errors
            .Where(error =>
                !string.IsNullOrWhiteSpace(error))
            .Select(error => new ApiError(
                null,
                error))
            .Distinct()
            .ToArray();
    }
}