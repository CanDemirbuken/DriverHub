using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.UserRole;

namespace DriverHub.Application.Interfaces.Identity;

public interface IUserRoleService
{
    Task<Result> AssignRoleAsync(AssignRoleRequest request, CancellationToken cancellationToken = default);
    Task<Result> RemoveRoleAsync(RemoveUserRoleRequest request, CancellationToken cancellationToken = default);
}