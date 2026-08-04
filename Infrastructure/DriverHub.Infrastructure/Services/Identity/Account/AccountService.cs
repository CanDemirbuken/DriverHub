using DriverHub.Application.Common.Constants;
using DriverHub.Application.Common.EmailTemplates;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Errors.Identity;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Communication.Mail;
using DriverHub.Application.Contracts.Identity.Account.EmailConfirmation;
using DriverHub.Application.Contracts.Identity.Account.Register;
using DriverHub.Application.Interfaces.Account;
using DriverHub.Application.Interfaces.Communication;
using DriverHub.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DriverHub.Infrastructure.Services.Identity.Account;

public sealed class AccountService(UserManager<AppUser> userManager, IMailService mailService, ILogger<AccountService> logger) : IAccountService
{
    public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user is null)
            return Result.Failure(Error.NotFound($"{request.UserId} kimlik bilgisine sahip kayıt bulunamadı."));

        if (user.EmailConfirmed)
            return Result.Success();

        string decodedToken;

        try
        {
            decodedToken = Encoding.UTF8.GetString(
                WebEncoders.Base64UrlDecode(request.ConfirmationToken));
        }
        catch (FormatException)
        {
            return Result.Failure(AccountErrors.InvalidEmailConfirmationToken);
        }

        IdentityResult confirmationResult = await userManager.ConfirmEmailAsync(user, decodedToken);
        if (!confirmationResult.Succeeded)
        {
            IReadOnlyCollection<Error> errors = IdentityErrorMapper
                .Map(confirmationResult.Errors)
                .Select(message => Error.Validation(
                    "Identity.Validation",
                    message))
                .ToArray();
            return Result.Failure(errors);
        }

        return Result.Success();
    }

    public async Task<Result<RegisterUserResponse>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        AppUser? existingUser = await userManager.FindByEmailAsync(request.Email);

        if (existingUser is not null)
            return Result<RegisterUserResponse>.Failure(AccountErrors.EmailAlreadyExists);

        var user = new AppUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
            IsActive = true,
            IsDeleted = false,
            CreatedDate = DateTime.UtcNow
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
                    : [AccountErrors.DefaultRoleAssignmentFailed]);
        }

        try
        {
            await SendEmailConfirmationAsync(user, cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Email confirmation mail could not be sent to user {UserId}.",
                user.Id);

            return Result<RegisterUserResponse>.Failure(AccountErrors.EmailConfirmationDeliveryFailed);
        }

        var response = new RegisterUserResponse(user.Id);

        return Result<RegisterUserResponse>.Success(response);
    }

    #region Private Methods
    private async Task SendEmailConfirmationAsync(AppUser user, CancellationToken cancellationToken)
    {
        string confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

        string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationToken));

        string mailBody = EmailConfirmationTemplate.Create(user.FirstName, user.Id, encodedToken);

        await mailService.SendAsync(new SendMailRequest(
                user.Email!,
                "Confirm your DriverHub email address",
                mailBody),
            cancellationToken);
    }
    #endregion
}
