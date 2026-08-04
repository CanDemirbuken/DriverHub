namespace DriverHub.Application.Contracts.Identity.Authentication.Login;

public sealed record LoginUserResponse(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);