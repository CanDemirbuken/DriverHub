using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Account.Register;

namespace DriverHub.Application.Interfaces.Account;

public interface IAccountService
{
    Task<Result<RegisterUserResponse>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);
}