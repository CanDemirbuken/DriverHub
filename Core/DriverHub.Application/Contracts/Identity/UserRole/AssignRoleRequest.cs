namespace DriverHub.Application.Contracts.Identity.UserRole;

public sealed record AssignRoleRequest(string UserId, string RoleId);