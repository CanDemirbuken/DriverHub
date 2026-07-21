using DriverHub.Persistence.Context;
using DriverHub.Persistence.Options.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DriverHub.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,IConfiguration configuration)
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

        return services;
    }
}