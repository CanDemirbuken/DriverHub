namespace DriverHub.Application.Contracts.Authentication.Login;

public sealed record LoginUserResponse(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);