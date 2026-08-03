using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication.Register;

namespace DriverHub.Application.Interfaces.Account;

public interface IAccountService
{
    Task<Result<RegisterUserResponse>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);
}