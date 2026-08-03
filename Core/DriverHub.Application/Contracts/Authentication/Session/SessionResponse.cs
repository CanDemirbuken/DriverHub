namespace DriverHub.Application.Contracts.Authentication.Session;

public sealed record SessionResponse(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);