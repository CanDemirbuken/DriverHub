namespace DriverHub.Application.Contracts.Identity.Token.RefreshToken;

public sealed record RefreshSessionResponse(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);