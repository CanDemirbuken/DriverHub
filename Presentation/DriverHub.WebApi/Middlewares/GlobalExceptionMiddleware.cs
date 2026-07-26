using DriverHub.WebApi.Models.Common;
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
        catch (OperationCanceledException)
            when (context.RequestAborted.IsCancellationRequested)
        {
            logger.LogInformation(
                "İstek istemci tarafından iptal edildi. Method: {RequestMethod}, Path: {RequestPath}, TraceId: {TraceId}",
                context.Request.Method,
                context.Request.Path.Value,
                context.TraceIdentifier);
        }
        catch (Exception exception)
        {
            await HandleUnexpectedExceptionAsync(context, exception);
        }
    }

    private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        IReadOnlyCollection<ApiError> errors =
            exception.Errors
                .Where(error =>
                    !string.IsNullOrWhiteSpace(error.ErrorMessage))
                .Select(error => new ApiError(
                    string.IsNullOrWhiteSpace(error.PropertyName)
                        ? null
                        : error.PropertyName,
                    error.ErrorMessage))
                .Distinct()
                .ToArray();

        var response = new ApiResponse<object>(
            false,
            null,
            errors);

        await WriteResponseAsync(
            context,
            StatusCodes.Status400BadRequest,
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

        var response = new ApiResponse<object>(
            false,
            null,
            [
                new ApiError(
                    null,
                    "Beklenmeyen bir hata oluştu.")
            ]);

        await WriteResponseAsync(
            context,
            StatusCodes.Status500InternalServerError,
            response);
    }

    private static async Task WriteResponseAsync<TResponse>(HttpContext context, int statusCode, TResponse response)
    {
        if (context.Response.HasStarted)
        {
            return;
        }

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(
            response,
            cancellationToken: context.RequestAborted);
    }
}