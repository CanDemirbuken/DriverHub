namespace DriverHub.WebApi.Models.Errors;

public sealed class ErrorResult : ErrorStatusCode
{
    public required string Message { get; init; }
}