using Microsoft.Extensions.DependencyInjection;

namespace DriverHub.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(typeof(AssemblyReference).Assembly);
        });

        return services;
    }
}