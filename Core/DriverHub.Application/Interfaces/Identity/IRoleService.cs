using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Role;

namespace DriverHub.Application.Interfaces.Identity;

public interface IRoleService
{
    Task<Result<CreateRoleResponse>> CreateAsync(CreateRoleRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(UpdateRoleRequest request, CancellationToken cancellationToken = default);
    Task<Result> RemoveAsync(RemoveRoleRequest request, CancellationToken cancellationToken = default);
}