using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication.Register;
using DriverHub.Application.Interfaces.Authentication;
using MediatR;

namespace DriverHub.Application.Features.Authentication.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler(IAuthenticationService identityService) : IRequestHandler<RegisterUserCommand, Result<RegisterUserCommandResponse>>
{
    public async Task<Result<RegisterUserCommandResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        RegisterUserRequest registerRequest = new(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        Result<RegisterUserResponse> result =
            await identityService.RegisterAsync(
                registerRequest,
                cancellationToken);

        if (result.IsFailure)
        {
            return Result<RegisterUserCommandResponse>.Failure(result.StatusCode, result.Errors);
        }

        RegisterUserCommandResponse response = new(result.Value.Id);
        return Result<RegisterUserCommandResponse>.Success(response, result.StatusCode);
    }
}