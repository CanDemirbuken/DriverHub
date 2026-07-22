namespace DriverHub.WebApi.Models.Errors;

public sealed class ValidationErrorDetails : ErrorStatusCode
{
    public required IReadOnlyCollection<string> Errors { get; init; }
}