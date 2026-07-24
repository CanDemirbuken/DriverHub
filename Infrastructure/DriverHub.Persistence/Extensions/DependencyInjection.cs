using DriverHub.Application.Interfaces;
using DriverHub.Application.Interfaces.QueryServices;
using DriverHub.Persistence.Context;
using DriverHub.Persistence.Options.Sql;
using DriverHub.Persistence.QueryServices;
using DriverHub.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DriverHub.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<SqlOptions>()
            .Bind(configuration.GetSection(SqlOptions.SectionName))
            .Validate(
                options => !string.IsNullOrWhiteSpace(options.ConnectionString),
                "SQL Server connection string boş bırakılamaz.")
            .ValidateOnStart();

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            SqlOptions sqlOptions = serviceProvider
                .GetRequiredService<IOptions<SqlOptions>>()
                .Value;

            options.UseSqlServer(sqlOptions.ConnectionString);
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICarRepository, CarRepository>();

        services.AddScoped<IAboutQueryService, AboutQueryService>();
        services.AddScoped<IBannerQueryService, BannerQueryService>();
        services.AddScoped<IBrandQueryService, BrandQueryService>();
        services.AddScoped<ICarQueryService, CarQueryService>();
        services.AddScoped<ICategoryQueryService, CategoryQueryService>();
        services.AddScoped<IContactQueryService, ContactQueryService>();
        services.AddScoped<IFeatureQueryService, FeatureQueryService>();

        services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<AppDbContext>());

        return services;
    }
}