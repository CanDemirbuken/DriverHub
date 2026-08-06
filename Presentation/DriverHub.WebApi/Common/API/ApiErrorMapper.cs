using DriverHub.Application.Common.Errors;
using DriverHub.WebApi.Common.API;

public static class ApiErrorMapper
{
    public static ApiError ToApiError(this Error error)
        => new(error.Code, error.Field, error.Message);

    public static IReadOnlyCollection<ApiError> ToApiErrors(this IEnumerable<Error> errors)
        => errors.Select(x => x.ToApiError()).ToArray();
}