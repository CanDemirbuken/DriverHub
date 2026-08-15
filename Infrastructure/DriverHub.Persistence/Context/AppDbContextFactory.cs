using DriverHub.Persistence.Options.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DriverHub.Persistence.Context;

public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        string basePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "..",
            "..",
            "Presentation",
            "DriverHub.WebApi");

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        string connectionString = configuration
            .GetSection(SqlOptions.SectionName)
            .GetValue<string>(nameof(SqlOptions.ConnectionString))
            ?? throw new InvalidOperationException(
                "SQL connection string bulunamadı.");

        DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();

        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}