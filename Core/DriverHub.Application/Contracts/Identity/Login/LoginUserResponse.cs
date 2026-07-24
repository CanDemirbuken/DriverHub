namespace DriverHub.Application.Contracts.Identity.Login;

public sealed record LoginUserResponse(string AccessToken, DateTime AccessTokenExpiresAt);