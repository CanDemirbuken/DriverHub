using DriverHub.Application.Features.Identity.RoleFeatures.Queries.GetAllRoles;
using DriverHub.Application.Features.Identity.RoleFeatures.Queries.GetRoleById;
using DriverHub.Application.Interfaces.QueryServices.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.QueryServices.Identity;

public sealed class RoleQueryService(RoleManager<IdentityRole> roleManager) : IRoleQueryService
{
    public async Task<IReadOnlyList<GetAllRolesQueryResponse>> GetRolesAsync(CancellationToken cancellationToken = default)
    {
        return await roleManager.Roles
            .AsNoTracking()
            .OrderBy(role => role.Name)
            .Select(role => new GetAllRolesQueryResponse(role.Id, role.Name!))
            .ToListAsync(cancellationToken);
    }

    public async Task<GetRoleByIdQueryResponse?> GetRoleByIdAsync(string roleId, CancellationToken cancellationToken = default)
    {
        IdentityRole? role = await roleManager.FindByIdAsync(roleId);
        if (role is null)
            return null;

        return new GetRoleByIdQueryResponse(role.Id, role.Name!);
    }
}