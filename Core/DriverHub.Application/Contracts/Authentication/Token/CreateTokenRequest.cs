namespace DriverHub.Application.Contracts.Authentication.Token;

public sealed record CreateTokenRequest(string UserId, string Email, IReadOnlyList<string> Roles);