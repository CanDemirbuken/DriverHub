namespace DriverHub.Application.Contracts.Identity.Session;

public sealed record SessionResponse(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);