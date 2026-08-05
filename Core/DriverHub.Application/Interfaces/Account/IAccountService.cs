using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Account.EmailConfirmation;
using DriverHub.Application.Contracts.Identity.Account.Password;
using DriverHub.Application.Contracts.Identity.Account.Register;

namespace DriverHub.Application.Interfaces.Account;

public interface IAccountService
{
    Task<Result<RegisterUserResponse>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);
    Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellationToken = default);
    Task<Result> ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken cancellationToken = default);
    Task<Result> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken cancellationToken = default);
}