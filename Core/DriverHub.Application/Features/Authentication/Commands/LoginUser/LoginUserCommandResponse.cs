namespace DriverHub.Application.Features.Authentication.Commands.LoginUser;

public sealed record LoginUserCommandResponse(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);