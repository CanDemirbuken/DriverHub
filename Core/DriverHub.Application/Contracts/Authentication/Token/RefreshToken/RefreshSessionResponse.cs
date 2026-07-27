namespace DriverHub.Application.Contracts.Authentication.Token.RefreshToken;

public sealed record RefreshSessionResponse(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);