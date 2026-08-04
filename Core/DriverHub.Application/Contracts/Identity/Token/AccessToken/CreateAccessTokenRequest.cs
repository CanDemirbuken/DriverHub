namespace DriverHub.Application.Contracts.Identity.Token.AccessToken;

public sealed record CreateAccessTokenRequest(string UserId, string Email, IReadOnlyCollection<string> Roles);