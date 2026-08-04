using DriverHub.Application.Common.Constants;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Role;
using DriverHub.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Infrastructure.Services.Identity.Role;

public sealed class RoleService(RoleManager<IdentityRole> roleManager) : IRoleService
{
    public async Task<Result<CreateRoleResponse>> CreateAsync(CreateRoleRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string roleName = request.Name.Trim();

        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (roleExists)
            return Result<CreateRoleResponse>.Failure(Error.Conflict($"{roleName} rolü zaten mevcut."));

        var role = new IdentityRole
        {
            Name = roleName
        };

        IdentityResult creationResult = await roleManager.CreateAsync(role);

        if (!creationResult.Succeeded)
        {
            IReadOnlyCollection<Error> errors = IdentityErrorMapper
                .Map(creationResult.Errors)
                .Select(message => Error.Validation(
                    "Identity.RoleCreationFailed",
                    message,
                    nameof(request.Name)))
                .ToArray();

            return Result<CreateRoleResponse>.Failure(errors);
        }

        return Result<CreateRoleResponse>.Success(new CreateRoleResponse(role.Id));
    }

    public async Task<Result> UpdateAsync(UpdateRoleRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        IdentityRole? role = await roleManager.FindByIdAsync(request.Id);

        if (role is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip rol bulunamadı."));

        if (IsSystemRole(role.Name))
            return Result.Failure(Error.Conflict($"{role.Name} sistem rolü güncellenemez."));

        string roleName = request.Name.Trim();
        string? normalizedRoleName = roleManager.NormalizeKey(roleName);

        bool roleExists = await roleManager.Roles
            .AsNoTracking()
            .AnyAsync(
                existingRole =>
                    existingRole.Id != role.Id &&
                    existingRole.NormalizedName == normalizedRoleName,
                cancellationToken);

        if (roleExists)
            return Result.Failure(
                Error.Conflict($"{roleName} rolü zaten mevcut."));

        role.Name = roleName;

        IdentityResult updateResult = await roleManager.UpdateAsync(role);

        if (!updateResult.Succeeded)
        {
            IReadOnlyCollection<Error> errors = IdentityErrorMapper
                .Map(updateResult.Errors)
                .Select(message => Error.Validation(
                    "Identity.RoleUpdateFailed",
                    message,
                    nameof(request.Name)))
                .ToArray();

            return Result.Failure(errors);
        }

        return Result.Success();
    }

    public async Task<Result> RemoveAsync(RemoveRoleRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        IdentityRole? role = await roleManager.FindByIdAsync(request.Id);

        if (role is null)
            return Result.Failure(Error.NotFound($"{request.Id} kimlik bilgisine sahip rol bulunamadı."));

        if (IsSystemRole(role.Name))
            return Result.Failure(Error.Conflict($"{role.Name} sistem rolü silinemez."));

        IdentityResult deletionResult = await roleManager.DeleteAsync(role);

        if (!deletionResult.Succeeded)
        {
            IReadOnlyCollection<Error> errors = IdentityErrorMapper
                .Map(deletionResult.Errors)
                .Select(message => Error.Failure(
                    "Identity.RoleRemovalFailed",
                    message))
                .ToArray();

            return Result.Failure(errors);
        }

        return Result.Success();
    }

    private static bool IsSystemRole(string? roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return false;

        return RoleNames.All.Contains(
            roleName,
            StringComparer.OrdinalIgnoreCase);
    }
}