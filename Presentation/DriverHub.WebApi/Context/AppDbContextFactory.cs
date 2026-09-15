using DriverHub.Persistence.Context;
using DriverHub.Persistence.Options.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DriverHub.WebApi.Context;

// Design-time configuration belongs to the startup project, which owns UserSecretsId.
public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        IConfiguration arguments = new ConfigurationBuilder().AddCommandLine(args).Build();
        string environment = arguments["environment"]
            ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
            ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? "Development";
        string basePath = arguments["contentRoot"] ?? FindStartupDirectory();

        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json", optional: true);

        if (string.Equals(environment, "Development", StringComparison.OrdinalIgnoreCase))
            builder.AddUserSecrets<AppDbContextFactory>(optional: true);

        IConfiguration configuration = builder.AddEnvironmentVariables().AddCommandLine(args).Build();
        string? connectionString = configuration[$"{SqlOptions.SectionName}:{nameof(SqlOptions.ConnectionString)}"];
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                "SqlOptions:ConnectionString boş. Development ortamında DriverHub.WebApi User Secrets ayarını " +
                "veya SqlOptions__ConnectionString ortam değişkenini tanımlayın. EF startup projesi DriverHub.WebApi olmalıdır.");

        return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connectionString).Options);
    }

    private static string FindStartupDirectory()
    {
        foreach (string start in new[] { Directory.GetCurrentDirectory(), AppContext.BaseDirectory })
        {
            for (DirectoryInfo? directory = new(start); directory is not null; directory = directory.Parent)
            {
                if (File.Exists(Path.Combine(directory.FullName, "DriverHub.WebApi.csproj")))
                    return directory.FullName;

                string candidate = Path.Combine(directory.FullName, "Presentation", "DriverHub.WebApi");
                if (File.Exists(Path.Combine(candidate, "DriverHub.WebApi.csproj")))
                    return candidate;
            }
        }

        throw new InvalidOperationException("DriverHub.WebApi dizini bulunamadı. --contentRoot ile startup dizinini belirtin.");
    }
}
