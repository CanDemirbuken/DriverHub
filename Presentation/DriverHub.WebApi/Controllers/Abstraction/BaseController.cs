using DriverHub.Application.Common.Errors;
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

    protected IActionResult ToActionResult(Result result, int successStatusCode = StatusCodes.Status200OK)
    {
        if (result.IsFailure)
            return CreateFailureResult(result.Errors);

        if (successStatusCode == StatusCodes.Status204NoContent)
            return NoContent();

        var response = new ApiResponse<object>(
            true,
            null,
            []);

        return StatusCode(successStatusCode, response);
    }

    protected IActionResult ToActionResult<TResponse>(Result<TResponse> result, int successStatusCode = StatusCodes.Status200OK)
    {
        if (result.IsFailure)
            return CreateFailureResult(result.Errors);

        var response = new ApiResponse<TResponse>(
            true,
            result.Value,
            []);

        return StatusCode(successStatusCode, response);
    }

    private IActionResult CreateFailureResult(IReadOnlyCollection<Error> errors)
    {
        int statusCode = MapStatusCode(errors);

        var response = new ApiResponse<object>(
            false,
            null,
            MapErrors(errors));

        return StatusCode(statusCode, response);
    }

    private static int MapStatusCode(IReadOnlyCollection<Error> errors)
    {
        if (errors.Count == 0)
            return StatusCodes.Status500InternalServerError;

        ErrorType[] errorTypes = errors
            .Select(error => error.Type)
            .Distinct()
            .ToArray();

        if (errorTypes.Length > 1)
            return StatusCodes.Status500InternalServerError;

        return errorTypes[0] switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Locked => StatusCodes.Status423Locked,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static IReadOnlyCollection<ApiError> MapErrors(IEnumerable<Error> errors)
    {
        return errors
            .Where(error => !string.IsNullOrWhiteSpace(error.Message))
            .Select(error => new ApiError(
                error.Code,
                error.Field,
                error.Message))
            .Distinct()
            .ToArray();
    }
}