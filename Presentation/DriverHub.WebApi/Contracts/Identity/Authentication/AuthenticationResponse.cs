namespace DriverHub.WebApi.Contracts.Identity.Authentication;

public sealed record AuthenticationResponse(
    string AccessToken,
    DateTime AccessTokenExpiresAt);