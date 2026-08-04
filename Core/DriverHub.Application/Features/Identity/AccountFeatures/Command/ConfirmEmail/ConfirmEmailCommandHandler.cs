using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Account.EmailConfirmation;
using DriverHub.Application.Interfaces.Account;
using MediatR;

namespace DriverHub.Application.Features.Identity.AccountFeatures.Command.ConfirmEmail;

public sealed class ConfirmEmailCommandHandler(IAccountService accountService) : IRequestHandler<ConfirmEmailCommand, Result>
{
    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var confirmEmailRequest = new ConfirmEmailRequest(request.UserId, request.ConfirmationToken);
        return await accountService.ConfirmEmailAsync(confirmEmailRequest, cancellationToken);
    }
}