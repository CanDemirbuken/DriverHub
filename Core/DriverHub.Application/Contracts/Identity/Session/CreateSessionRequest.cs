namespace DriverHub.Application.Contracts.Identity.Session;

public sealed record CreateSessionRequest(string UserId, string Email, IReadOnlyCollection<string> Roles);