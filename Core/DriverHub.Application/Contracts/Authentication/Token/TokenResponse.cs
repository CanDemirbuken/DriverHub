namespace DriverHub.Application.Contracts.Authentication.Token;

public sealed record TokenResponse(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);