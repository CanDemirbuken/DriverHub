using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace DriverHub.WebApi.Extensions;

public static class LoggingExtensions
{
    public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString =
            configuration
                .GetSection("SqlOptions")
                .GetValue<string>("ConnectionString")
            ?? throw new InvalidOperationException(
                "SQL Server connection string bulunamadı.");

        MSSqlServerSinkOptions sinkOptions = new()
        {
            TableName = "Logs",
            SchemaName = "dbo",

            // Development aşamasında true kullanılabilir.
            // Production ortamında tabloyu migration/script ile
            // oluşturup false yapılması daha güvenlidir.
            AutoCreateSqlTable = true,

            BatchPostingLimit = 50,
            BatchPeriod = TimeSpan.FromSeconds(5)
        };

        ColumnOptions columnOptions = new();

        columnOptions.Store.Add(StandardColumn.LogEvent);
        columnOptions.Store.Add(StandardColumn.TraceId);
        columnOptions.Store.Add(StandardColumn.SpanId);

        services.AddSerilog(loggerConfiguration =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty(
                    "Application",
                    "DriverHub")

                .WriteTo.Console(
                    restrictedToMinimumLevel:
                        LogEventLevel.Warning)

                .WriteTo.File(
                    path: "Logs/log-.txt",
                    restrictedToMinimumLevel:
                        LogEventLevel.Warning,
                    rollingInterval:
                        RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: 10_000_000,
                    shared: true)

                .WriteTo.MSSqlServer(
                    connectionString: connectionString,
                    sinkOptions: sinkOptions,
                    restrictedToMinimumLevel:
                        LogEventLevel.Warning,
                    columnOptions: columnOptions);
        });

        return services;
    }
}