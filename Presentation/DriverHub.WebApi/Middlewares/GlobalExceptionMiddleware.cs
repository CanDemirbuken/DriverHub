using DriverHub.Application.Common.Errors;
using DriverHub.WebApi.Common.API;

namespace DriverHub.WebApi.Middlewares;

public sealed class GlobalExceptionMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
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

    private async Task HandleUnexpectedExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(
            exception,
            "Beklenmeyen bir hata oluştu. Method: {RequestMethod}, Path: {RequestPath}, TraceId: {TraceId}",
            context.Request.Method,
            context.Request.Path.Value,
            context.TraceIdentifier);

        Error error = Error.Failure(
            "Server.Unexpected",
            "Beklenmeyen bir hata oluştu.");

        ApiResponse<object> response = new(
            false,
            null,
            [error.ToApiError()]);

        await WriteResponseAsync(
            context,
            StatusCodes.Status500InternalServerError,
            response);
    }

    private static async Task WriteResponseAsync<TResponse>(HttpContext context, int statusCode, TResponse response)
    {
        if (context.Response.HasStarted)
            return;

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(
            response,
            cancellationToken: context.RequestAborted);
    }
}