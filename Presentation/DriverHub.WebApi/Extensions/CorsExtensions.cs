namespace DriverHub.WebApi.Extensions;

public static class CorsExtensions
{
    private const string CorsPolicyName = "Frontend";

    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicyName, policy =>
            {
                policy
                    .WithOrigins("http://localhost:4001")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }

    public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicyName);
        return app;
    }
}