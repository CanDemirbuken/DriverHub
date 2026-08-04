using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Authentication.Login;

namespace DriverHub.Application.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<Result<LoginUserResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken = default);
}