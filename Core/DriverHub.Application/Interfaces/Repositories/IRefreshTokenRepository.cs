using DriverHub.Application.Contracts.Authentication.Token;

namespace DriverHub.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task CreateAsync(StoreRefreshTokenRequest request, CancellationToken cancellationToken = default);
}