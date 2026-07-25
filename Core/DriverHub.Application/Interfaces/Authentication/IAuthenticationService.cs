using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication.Login;
using DriverHub.Application.Contracts.Authentication.Register;

namespace DriverHub.Application.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<Result<RegisterUserResponse>> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<LoginUserResponse>> LoginAsync(
        LoginUserRequest request,
        CancellationToken cancellationToken = default);
}