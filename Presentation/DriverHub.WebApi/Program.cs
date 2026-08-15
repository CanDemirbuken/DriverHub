using DriverHub.Application.Extensions;
using DriverHub.Infrastructure.Extensions;
using DriverHub.Infrastructure.Services.Identity;
using DriverHub.Persistence.Extensions;
using DriverHub.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);

    builder.Services.AddSerilogLogging(builder.Configuration);

    builder.Services.AddControllers();

    builder.Services.AddSwaggerDocumentation();

    builder.Services.AddApplication();
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddCorsConfiguration();

    builder.Services.AddApplicationAuthorization();

    builder.Services.AddHealthCheckServices();
    builder.Services.AddRateLimitServices();

    WebApplication app = builder.Build();

    await using (AsyncServiceScope scope =
    app.Services.CreateAsyncScope())
    {
        IdentitySeeder identitySeeder =
            scope.ServiceProvider
                .GetRequiredService<IdentitySeeder>();

        await identitySeeder.SeedAsync();
    }

    app.UseGlobalExceptionMiddleware();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerDocumentation();
    }

    app.UseHttpsRedirection();
    app.UseCorsConfiguration();

    app.UseRateLimiter();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.MapHealthCheckEndpoints();

    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Uygulama beklenmeyen şekilde sonlandırıldı.");
}
finally
{
    Log.CloseAndFlush();
}