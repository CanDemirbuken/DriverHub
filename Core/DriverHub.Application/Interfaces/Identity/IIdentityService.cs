using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Login;
using DriverHub.Application.Contracts.Identity.Register;

namespace DriverHub.Application.Interfaces.Identity;

public interface IIdentityService
{
    Task<Result<RegisterUserResponse>> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default);

    Task<Result<LoginUserResponse>> LoginAsync(
        LoginUserRequest request,
        CancellationToken cancellationToken = default);
}