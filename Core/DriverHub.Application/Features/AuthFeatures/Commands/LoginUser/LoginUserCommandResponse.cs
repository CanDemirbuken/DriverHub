namespace DriverHub.Application.Features.AuthFeatures.Commands.LoginUser;

public sealed record LoginUserCommandResponse(string AccessToken, DateTime AccessTokenExpiresAt);