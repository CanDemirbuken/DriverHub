namespace DriverHub.Application.Contracts.Identity.UserRole;

public sealed record RemoveUserRoleRequest(string UserId, string RoleId);