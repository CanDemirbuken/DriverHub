using DriverHub.Application.Common.Results;
using DriverHub.Application.Interfaces.Account;
using MediatR;

namespace DriverHub.Application.Features.Identity.AccountFeatures.Command.ForgotPassword;

public sealed class ForgotPasswordCommandHandler(IAccountService accountService) : IRequestHandler<ForgotPasswordCommand, Result>
{
    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        return await accountService.ForgotPasswordAsync(new(request.Email), cancellationToken);
    }
}