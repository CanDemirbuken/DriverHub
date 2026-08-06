namespace DriverHub.WebApi.Common.API;

public sealed record ApiError(string Code, string? Field, string Message);