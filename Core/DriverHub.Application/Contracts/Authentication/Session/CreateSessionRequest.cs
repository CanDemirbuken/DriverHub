namespace DriverHub.Application.Contracts.Authentication.Session;

public sealed record CreateSessionRequest(string UserId, string Email, IReadOnlyCollection<string> Roles);