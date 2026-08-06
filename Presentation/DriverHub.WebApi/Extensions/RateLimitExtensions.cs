using System.Globalization;
using System.Threading.RateLimiting;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.WebApi.Common.API;
using DriverHub.WebApi.Common.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace DriverHub.WebApi.Extensions;

public static class RateLimitExtensions
{
    public static IServiceCollection AddRateLimitServices(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.OnRejected = HandleRejectedRequestAsync;

            AddFixedWindowPolicy(options, RateLimitPolicyNames.Login, RateLimitPolicies.Login);
            AddFixedWindowPolicy(options, RateLimitPolicyNames.Registration, RateLimitPolicies.Registration);
            AddFixedWindowPolicy(options, RateLimitPolicyNames.ConfirmEmail, RateLimitPolicies.ConfirmEmail);
            AddFixedWindowPolicy(options, RateLimitPolicyNames.ForgotPassword, RateLimitPolicies.ForgotPassword);
            AddFixedWindowPolicy(options, RateLimitPolicyNames.ResetPassword, RateLimitPolicies.ResetPassword);
            AddFixedWindowPolicy(options, RateLimitPolicyNames.RefreshToken, RateLimitPolicies.RefreshToken);
        });

        return services;
    }

    private static void AddFixedWindowPolicy(RateLimiterOptions options, string policyName, FixedWindowPolicyOptions policyOptions)
    {
        options.AddPolicy(
            policyName,
            httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: GetClientIpAddress(httpContext),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = policyOptions.PermitLimit,
                        Window = policyOptions.Window,
                        QueueLimit = policyOptions.QueueLimit,
                        AutoReplenishment = true
                    }));
    }

    private static async ValueTask HandleRejectedRequestAsync(OnRejectedContext context, CancellationToken cancellationToken)
    {
        HttpResponse response = context.HttpContext.Response;

        response.StatusCode = StatusCodes.Status429TooManyRequests;
        response.ContentType = "application/json";

        if (context.Lease.TryGetMetadata(
            MetadataName.RetryAfter,
            out TimeSpan retryAfter))
        {
            response.Headers.RetryAfter = Math
                .Ceiling(retryAfter.TotalSeconds)
                .ToString(CultureInfo.InvariantCulture);
        }

        Error error = Error.TooManyRequests("Çok fazla istek gönderdiniz. Lütfen kısa bir süre sonra tekrar deneyin.");

        ApiResponse<object> apiResponse = new(
            false,
            null,
            [error.ToApiError()]);

        await response.WriteAsJsonAsync(apiResponse, cancellationToken);
    }

    private static string GetClientIpAddress(HttpContext httpContext)
    {
        return httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }
}