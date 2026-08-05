using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Account.Password;
using DriverHub.Application.Interfaces.Account;
using MediatR;

namespace DriverHub.Application.Features.Identity.AccountFeatures.Command.ResetPassword;

public sealed class ResetPasswordCommandHandler(IAccountService accountService) : IRequestHandler<ResetPasswordCommand, Result>
{
    public Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var passwordResetRequest = new ResetPasswordRequest(request.Email, request.ResetToken, request.NewPassword);
        return accountService.ResetPasswordAsync(passwordResetRequest, cancellationToken);
    }
}