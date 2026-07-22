using DriverHub.Application.Exceptions;
using DriverHub.WebApi.Models.Errors;
using FluentValidation;

namespace DriverHub.WebApi.Middlewares;

public sealed class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException exception)
        {
            await HandleValidationExceptionAsync(context, exception);
        }
        catch (NotFoundException exception)
        {
            await HandleNotFoundExceptionAsync(context, exception);
        }
        catch (Exception exception)
        {
            await HandleUnexpectedExceptionAsync(context, exception);
        }
    }

    private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        string[] errors = exception.Errors
            .Select(error => error.ErrorMessage)
            .Where(errorMessage =>
                !string.IsNullOrWhiteSpace(errorMessage))
            .Distinct()
            .ToArray();

        ValidationErrorDetails response = new()
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Errors = errors
        };

        await WriteResponseAsync(
            context,
            StatusCodes.Status400BadRequest,
            response);
    }

    private static async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
    {
        ErrorResult response = new()
        {
            StatusCode = StatusCodes.Status404NotFound,
            Message = exception.Message
        };

        await WriteResponseAsync(
            context,
            StatusCodes.Status404NotFound,
            response);
    }

    private async Task HandleUnexpectedExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(
            exception,
            "Beklenmeyen bir hata oluştu. Method: {RequestMethod}, Path: {RequestPath}, TraceId: {TraceId}",
            context.Request.Method,
            context.Request.Path.Value,
            context.TraceIdentifier);

        ErrorResult response = new()
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "Beklenmeyen bir hata oluştu."
        };

        await WriteResponseAsync(
            context,
            StatusCodes.Status500InternalServerError,
            response);
    }

    private static async Task WriteResponseAsync<TResponse>(HttpContext context, int statusCode, TResponse response)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(response);
    }
}