using DriverHub.Application.Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace DriverHub.WebApi.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build())
            .AddPolicy(AuthorizationPolicies.AdminOnly, policy =>
                policy.RequireRole(RoleNames.Admin));

        return services;
    }
}