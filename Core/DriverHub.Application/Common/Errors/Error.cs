namespace DriverHub.Application.Common.Errors;

public sealed record Error(string Code, string Message, ErrorType Type, string? Field = null)
{
    public static Error Validation(string code, string message, string? field = null)
        => new(code, message, ErrorType.Validation, field);

    public static Error NotFound(string message, string? field = null)
        => new("Resource.NotFound", message, ErrorType.NotFound, field);

    public static Error Conflict(string message, string? field = null)
        => new("Resource.Conflict", message, ErrorType.Conflict, field);

    public static Error Unauthorized(string message, string? field = null)
        => new("Authentication.Unauthorized", message, ErrorType.Unauthorized, field);

    public static Error Forbidden(string message, string? field = null)
        => new("Authorization.Forbidden", message, ErrorType.Forbidden, field);

    public static Error Locked(string message, string? field = null)
        => new("Authentication.Locked", message, ErrorType.Locked, field);

    public static Error Failure(string message, string? field = null)
        => new("Server.Failure", message, ErrorType.Failure, field);

    public static Error TooManyRequests(string message, string? field = null)
        => new("RateLimit.TooManyRequests", message, ErrorType.TooManyRequests, field);
}