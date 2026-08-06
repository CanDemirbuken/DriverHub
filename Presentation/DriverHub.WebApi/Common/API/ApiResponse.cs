namespace DriverHub.WebApi.Common.API;

public sealed record ApiResponse<T>(bool IsSuccess, T? Data, IReadOnlyCollection<ApiError> Errors);