using DriverHub.Application.Features.Identity.RoleFeatures.Queries.GetAllRoles;
using DriverHub.Application.Features.Identity.RoleFeatures.Queries.GetRoleById;

namespace DriverHub.Application.Interfaces.QueryServices.Identity;

public interface IRoleQueryService
{
    Task<IReadOnlyList<GetAllRolesQueryResponse>> GetRolesAsync(CancellationToken cancellationToken = default);
    Task<GetRoleByIdQueryResponse> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken = default);
}