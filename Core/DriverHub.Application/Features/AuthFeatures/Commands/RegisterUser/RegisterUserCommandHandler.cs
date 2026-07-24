using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Register;
using DriverHub.Application.Interfaces.Identity;
using MediatR;

namespace DriverHub.Application.Features.AuthFeatures.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler(IIdentityService identityService) : IRequestHandler<RegisterUserCommand, Result<RegisterUserCommandResponse>>
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
            return Result<RegisterUserCommandResponse>.Failure(result.Errors);
        }

        RegisterUserCommandResponse response = new(result.Value.Id);
        return Result<RegisterUserCommandResponse>.Success(response);
    }
}