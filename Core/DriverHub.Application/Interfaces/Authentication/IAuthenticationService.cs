using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication.Login;
using DriverHub.Application.Contracts.Authentication.Register;
using DriverHub.Application.Contracts.Authentication.Token.RefreshToken;

namespace DriverHub.Application.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<Result<LoginUserResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken = default);
    Task<Result<RegisterUserResponse>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);
    Task<Result<RefreshSessionResponse>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}