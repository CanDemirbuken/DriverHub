using DriverHub.Application.Exceptions;
using DriverHub.WebApi.Models.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

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
        catch (ConflictException exception)
        {
            await HandleConflictExceptionAsync(context, exception);
        }
        catch (Exception exception)
        {
            await HandleUnexpectedExceptionAsync(context, exception);
        }
    }

    private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        Dictionary<string, string[]> errors = exception.Errors
            .Where(error =>
                !string.IsNullOrWhiteSpace(error.PropertyName) &&
                !string.IsNullOrWhiteSpace(error.ErrorMessage))
            .GroupBy(error => error.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group
                    .Select(error => error.ErrorMessage)
                    .Distinct()
                    .ToArray());

        ValidationProblemDetails response = new(errors)
        {
            Type = "https://httpstatuses.com/400",
            Title = "Validation failed",
            Status = StatusCodes.Status400BadRequest,
            Detail = "Bir veya daha fazla validation hatası oluştu.",
            Instance = context.Request.Path
        };

        response.Extensions["traceId"] = context.TraceIdentifier;

        await WriteResponseAsync(
            context,
            StatusCodes.Status400BadRequest,
            response);
    }

    private static async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
    {
        ProblemDetails response = new()
        {
            Type = "https://httpstatuses.com/404",
            Title = "Resource not found",
            Status = StatusCodes.Status404NotFound,
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        response.Extensions["traceId"] = context.TraceIdentifier;

        await WriteResponseAsync(
            context,
            StatusCodes.Status404NotFound,
            response);
    }

    private static async Task HandleConflictExceptionAsync(HttpContext context, ConflictException exception)
    {
        ProblemDetails response = new()
        {
            Type = "https://httpstatuses.com/409",
            Title = "Conflict",
            Status = StatusCodes.Status409Conflict,
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        response.Extensions["traceId"] = context.TraceIdentifier;

        await WriteResponseAsync(
            context,
            StatusCodes.Status409Conflict,
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

        ProblemDetails response = new()
        {
            Type = "https://httpstatuses.com/500",
            Title = "Internal server error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "Beklenmeyen bir hata oluştu.",
            Instance = context.Request.Path
        };

        response.Extensions["traceId"] = context.TraceIdentifier;

        await WriteResponseAsync(
            context,
            StatusCodes.Status500InternalServerError,
            response);
    }

    private static async Task WriteResponseAsync<TResponse>(HttpContext context, int statusCode, TResponse response)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(response);
    }
}