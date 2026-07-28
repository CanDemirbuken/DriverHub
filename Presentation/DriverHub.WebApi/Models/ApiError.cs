namespace DriverHub.WebApi.Models.Common;

public sealed record ApiError(string Code, string? Field, string Message);