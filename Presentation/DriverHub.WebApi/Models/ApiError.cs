namespace DriverHub.WebApi.Models.Common;

public sealed record ApiError(string? PropertyName, string Message);