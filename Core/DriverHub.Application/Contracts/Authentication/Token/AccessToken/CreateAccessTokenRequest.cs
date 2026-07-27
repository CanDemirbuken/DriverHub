namespace DriverHub.Application.Contracts.Authentication.Token.AccessToken;

public sealed record CreateAccessTokenRequest(string UserId, string Email, IReadOnlyCollection<string> Roles);