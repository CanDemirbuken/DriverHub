using DriverHub.Application.Common.Constants;
using DriverHub.Infrastructure.Options;
using DriverHub.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DriverHub.Infrastructure.Services.Identity;

public sealed class IdentitySeeder(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IOptions<IdentitySeedOptions> options)
{
    private readonly IdentitySeedOptions _options = options.Value;

    public async Task SeedAsync()
    {
        await SeedRolesAsync();
        await SeedAdminUserAsync();
    }

    private async Task SeedRolesAsync()
    {
        foreach (string roleName in RoleNames.All)
        {
            bool roleExists =
                await roleManager.RoleExistsAsync(roleName);

            if (roleExists)
                continue;

            IdentityResult roleResult =
                await roleManager.CreateAsync(
                    new IdentityRole(roleName));

            if (!roleResult.Succeeded)
            {
                string errors = string.Join(
                    ", ",
                    roleResult.Errors.Select(error => error.Description));

                throw new InvalidOperationException(
                    $"'{roleName}' rolü oluşturulamadı: {errors}");
            }
        }
    }

    private async Task SeedAdminUserAsync()
    {
        AppUser? adminUser =
            await userManager.FindByEmailAsync(
                _options.AdminEmail);

        if (adminUser is null)
        {
            adminUser = new AppUser
            {
                FirstName = _options.AdminFirstName,
                LastName = _options.AdminLastName,
                Email = _options.AdminEmail,
                UserName = _options.AdminEmail,
                EmailConfirmed = true,
                IsActive = true,
                IsDeleted = false
            };

            IdentityResult creationResult =
                await userManager.CreateAsync(
                    adminUser,
                    _options.AdminPassword);

            if (!creationResult.Succeeded)
            {
                string errors = string.Join(
                    ", ",
                    creationResult.Errors.Select(
                        error => error.Description));

                throw new InvalidOperationException(
                    $"Admin kullanıcısı oluşturulamadı: {errors}");
            }
        }

        bool isAdmin =
            await userManager.IsInRoleAsync(
                adminUser,
                RoleNames.Admin);

        if (!isAdmin)
        {
            IdentityResult roleResult =
                await userManager.AddToRoleAsync(
                    adminUser,
                    RoleNames.Admin);

            if (!roleResult.Succeeded)
            {
                string errors = string.Join(
                    ", ",
                    roleResult.Errors.Select(
                        error => error.Description));

                throw new InvalidOperationException(
                    $"Admin rolü kullanıcıya atanamadı: {errors}");
            }
        }
    }
}