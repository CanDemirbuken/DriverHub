using DriverHub.Application.Common.Constants;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication.Register;
using DriverHub.Application.Interfaces.Account;
using DriverHub.Infrastructure.Services.Identity;
using DriverHub.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace DriverHub.Infrastructure.Services.Account;

public sealed class AccountService(UserManager<AppUser> userManager) : IAccountService
{
    public async Task<Result<RegisterUserResponse>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        AppUser? existingUser = await userManager.FindByEmailAsync(request.Email);

        if (existingUser is not null)
            return Result<RegisterUserResponse>.Failure(AuthenticationErrors.EmailAlreadyExists);

        var user = new AppUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
            IsActive = true,
            IsDeleted = false
        };

        IdentityResult creationResult = await userManager.CreateAsync(user, request.Password);

        if (!creationResult.Succeeded)
        {
            IReadOnlyCollection<Error> errors = IdentityErrorMapper
                .Map(creationResult.Errors)
                .Select(message => Error.Validation(
                    "Identity.Validation",
                    message))
                .ToArray();

            return Result<RegisterUserResponse>.Failure(errors);
        }

        IdentityResult roleResult = await userManager.AddToRoleAsync(user, RoleNames.User);

        if (!roleResult.Succeeded)
        {
            IdentityResult deletionResult = await userManager.DeleteAsync(user);

            if (!deletionResult.Succeeded)
                throw new InvalidOperationException($"Kullanıcı oluşturuldu ancak '{RoleNames.User}' rolü atanamadı ve kullanıcı kaydı geri alınamadı.");

            IReadOnlyCollection<Error> errors = IdentityErrorMapper
                .Map(roleResult.Errors)
                .Select(message => Error.Failure(message))
                .ToArray();

            return Result<RegisterUserResponse>.Failure(
                errors.Count > 0
                    ? errors
                    : [AuthenticationErrors.DefaultRoleAssignmentFailed]);
        }

        var response = new RegisterUserResponse(user.Id);

        return Result<RegisterUserResponse>.Success(response);
    }
}
