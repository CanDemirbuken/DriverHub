using DriverHub.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace DriverHub.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(typeof(AssemblyReference).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
        services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);

        return services;
    }
}