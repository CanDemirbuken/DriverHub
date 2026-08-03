using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication.Register;
using DriverHub.Application.Interfaces.Account;
using MediatR;

namespace DriverHub.Application.Features.Identity.AccountFeatures.Command.RegisterUser;

public sealed class RegisterUserCommandHandler(IAccountService accountService) : IRequestHandler<RegisterUserCommand, Result<RegisterUserCommandResponse>>
{
    public async Task<Result<RegisterUserCommandResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        RegisterUserRequest registerRequest = new(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        Result<RegisterUserResponse> result =
            await accountService.RegisterAsync(registerRequest, cancellationToken);

        if (result.IsFailure)
            return Result<RegisterUserCommandResponse>.Failure(result.Errors);

        RegisterUserCommandResponse response = new(result.Value.Id);

        return Result<RegisterUserCommandResponse>.Success(response);
    }
}