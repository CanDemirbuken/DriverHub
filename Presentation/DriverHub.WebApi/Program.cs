using DriverHub.Application.Extensions;
using DriverHub.Persistence.Extensions;
using DriverHub.WebApi.Extensions;
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

    WebApplication app = builder.Build();

    app.UseGlobalExceptionMiddleware();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerDocumentation();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

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