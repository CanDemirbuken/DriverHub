namespace DriverHub.WebApi.Models.Common;

public sealed record ApiResponse<T>(bool IsSuccess, T? Data, IReadOnlyCollection<ApiError> Errors);