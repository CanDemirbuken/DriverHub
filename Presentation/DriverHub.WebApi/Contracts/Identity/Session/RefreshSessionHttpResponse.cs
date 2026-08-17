namespace DriverHub.WebApi.Contracts.Identity.Session;

public sealed record RefreshSessionHttpResponse(
    string AccessToken,
    DateTime AccessTokenExpiresAt);