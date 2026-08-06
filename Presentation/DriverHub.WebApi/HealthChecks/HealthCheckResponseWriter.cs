using System.Text.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DriverHub.WebApi.HealthChecks;

public static class HealthCheckResponseWriter
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public static Task WriteResponseAsync(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = report.Status.ToString(),
            totalDuration = report.TotalDuration.TotalMilliseconds,
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                description = entry.Value.Description,
                duration = entry.Value.Duration.TotalMilliseconds,
                tags = entry.Value.Tags,
                error = entry.Value.Exception?.Message
            })
        };

        return context.Response.WriteAsync(
            JsonSerializer.Serialize(response, SerializerOptions));
    }
}