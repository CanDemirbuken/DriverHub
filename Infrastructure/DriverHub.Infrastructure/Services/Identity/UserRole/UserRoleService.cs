using DriverHub.Application.Common.Constants;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.UserRole;
using DriverHub.Application.Interfaces.Identity;
using DriverHub.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace DriverHub.Infrastructure.Services.Identity.UserRole;

public sealed class UserRoleService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) : IUserRoleService
{
    public async Task<Result> AssignRoleAsync(AssignRoleRequest request, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user is null)
            return Result.Failure(Error.NotFound($"{request.UserId} kimlik bilgisine sahip kullanıcı bulunamadı."));

        var role = await roleManager.FindByIdAsync(request.RoleId);
        if (role is null)
            return Result.Failure(Error.NotFound($"{request.RoleId} kimlik bilgisine sahip rol bulunamadı."));

        var userHasRole = await userManager.IsInRoleAsync(user, role.Name!);
        if (userHasRole)
            return Result.Failure(Error.Conflict($"Kullanıcı zaten {role.Name} rolüne sahip."));

        var result = await userManager.AddToRoleAsync(user, role.Name!);
        if (!result.Succeeded)
        {
            IReadOnlyCollection<Error> errors = IdentityErrorMapper
                .Map(result.Errors)
                .Select(message => Error.Failure(message))
                .ToArray();

            return Result.Failure(errors);
        }

        return Result.Success();
    }

    public async Task<Result> RemoveRoleAsync(RemoveUserRoleRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await userManager.FindByIdAsync(request.UserId);

        if (user is null)
            return Result.Failure(Error.NotFound($"{request.UserId} kimlik bilgisine sahip kullanıcı bulunamadı."));

        var role = await roleManager.FindByIdAsync(request.RoleId);

        if (role is null || string.IsNullOrWhiteSpace(role.Name))
            return Result.Failure(Error.NotFound($"{request.RoleId} kimlik bilgisine sahip rol bulunamadı."));

        bool userHasRole = await userManager.IsInRoleAsync(user, role.Name);

        if (!userHasRole)
            return Result.Failure(Error.Conflict($"Kullanıcı {role.Name} rolüne sahip değildir."));

        IList<string> userRoles = await userManager.GetRolesAsync(user);

        bool isLastRole = userRoles.Count == 1;
        if (isLastRole)
            return Result.Failure(Error.Conflict("Kullanıcının en az bir rolü olmalıdır."));

        bool removingAdminRole = string.Equals(
            role.Name,
            RoleNames.Admin,
            StringComparison.OrdinalIgnoreCase);

        if (removingAdminRole)
        {
            IList<AppUser> adminUsers = await userManager.GetUsersInRoleAsync(RoleNames.Admin);

            if (adminUsers.Count == 1)
                return Result.Failure(Error.Conflict("Sistemde en az bir yönetici bulunmalıdır."));
        }

        IdentityResult removalResult = await userManager.RemoveFromRoleAsync(user, role.Name);

        if (!removalResult.Succeeded)
        {
            IReadOnlyCollection<Error> errors = IdentityErrorMapper
                .Map(removalResult.Errors)
                .Select(message => Error.Failure(message))
                .ToArray();

            return Result.Failure(errors);
        }

        return Result.Success();
    }
}