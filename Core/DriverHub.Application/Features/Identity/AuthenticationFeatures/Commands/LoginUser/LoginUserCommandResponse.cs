namespace DriverHub.Application.Features.Identity.AuthenticationFeatures.Commands.LoginUser;

public sealed record LoginUserCommandResponse(string AccessToken, DateTime AccessTokenExpiresAt, string RefreshToken, DateTime RefreshTokenExpiresAt);