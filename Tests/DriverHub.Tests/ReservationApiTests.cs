using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Encodings.Web;
using DriverHub.Application.Common.Constants;
using DriverHub.Application.Extensions;
using DriverHub.Application.Features.Entities.Reservations.Commands.CreateReservation;
using DriverHub.Domain.Entities;
using DriverHub.Persistence.Extensions;
using DriverHub.WebApi.Common.API;
using DriverHub.WebApi.Controllers.Entities;
using DriverHub.WebApi.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DriverHub.Tests;

public sealed class ReservationApiTests(ReservationDatabase database) : IClassFixture<ReservationDatabase>
{
    [Fact]
    public async Task Management_endpoints_enforce_authorization_and_server_identity()
    {
        await using var context = database.CreateContext();
        var seed = await database.SeedAsync(context);
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions { EnvironmentName = "Testing" });
        builder.Logging.ClearProviders();
        builder.WebHost.UseUrls("http://127.0.0.1:0");
        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?> { ["SqlOptions:ConnectionString"] = database.ConnectionString });
        builder.Services.AddApplication();
        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddControllers().AddApplicationPart(typeof(ReservationsController).Assembly);
        builder.Services.AddAuthentication("Test").AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("Test", _ => { });
        builder.Services.AddApplicationAuthorization();
        await using var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        await app.StartAsync();
        using var client = new HttpClient { BaseAddress = new Uri(app.Urls.Single()) };

        foreach (string path in new[] { "/api/reservations", $"/api/reservations/{Guid.NewGuid()}" })
        {
            Assert.Equal(HttpStatusCode.Unauthorized, (await client.GetAsync(path)).StatusCode);
        }
        client.DefaultRequestHeaders.Add("X-Test-User", seed.UserId);
        client.DefaultRequestHeaders.Add("X-Test-Role", "Customer");
        Assert.Equal(HttpStatusCode.Forbidden, (await client.GetAsync("/api/reservations")).StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, (await client.GetAsync($"/api/reservations/{Guid.NewGuid()}")).StatusCode);
        foreach (string action in new[] { "approve", "cancel" })
            Assert.Equal(HttpStatusCode.Forbidden, (await client.PostAsJsonAsync($"/api/reservations/{Guid.NewGuid()}/{action}", new { })).StatusCode);

        client.DefaultRequestHeaders.Remove("X-Test-Role");
        client.DefaultRequestHeaders.Add("X-Test-Role", RoleNames.Admin);
        var response = await client.PostAsJsonAsync("/api/reservations", new
        {
            seed.CarId, PickupLocationId = seed.LocationId, StartDate = seed.Start, EndDate = seed.Start.AddDays(3),
            ExtraIds = Array.Empty<Guid>(), UserId = "spoofed-user", CustomerEmail = "attacker@example.test",
            Status = 2, TotalPrice = 0
        });
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var created = await response.Content.ReadFromJsonAsync<ApiResponse<CreateReservationCommandResponse>>();
        var reservation = await context.Set<Reservation>().SingleAsync(r => r.Id == created!.Data!.Id);
        Assert.Equal(seed.UserId, reservation.UserId);
        Assert.Equal(seed.UserId + "@example.test", reservation.CustomerEmail);
        Assert.Equal(300m, reservation.TotalPrice);
        Assert.Equal(1, (int)reservation.Status);
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/api/reservations")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync($"/api/reservations/{reservation.Id}")).StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, (await client.GetAsync("/api/reservations?PageSize=101")).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync($"/api/reservations/{Guid.NewGuid()}")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await client.PostAsJsonAsync($"/api/reservations/{reservation.Id}/approve", new { })).StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, (await client.PostAsJsonAsync($"/api/reservations/{reservation.Id}/cancel", new { })).StatusCode);
        await app.StopAsync();
    }

    private sealed class TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string? user = Request.Headers["X-Test-User"].FirstOrDefault();
            if (user is null) return Task.FromResult(AuthenticateResult.NoResult());
            var identity = new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, user), new Claim(ClaimTypes.Role, Request.Headers["X-Test-Role"].ToString())], Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name)));
        }
    }
}
