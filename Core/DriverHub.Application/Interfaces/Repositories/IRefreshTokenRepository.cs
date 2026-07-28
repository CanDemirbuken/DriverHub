using DriverHub.Application.Contracts.Authentication.Token.RefreshToken;

namespace DriverHub.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task CreateAsync(StoreRefreshTokenRequest request, CancellationToken cancellationToken = default);
    Task<RefreshTokenRecord?> GetByHashAsync(string tokenHash, CancellationToken cancellationToken = default);
    Task<bool> RotateAsync(int currentTokenId, StoreRefreshTokenRequest newToken, DateTime revokedDate, CancellationToken cancellationToken = default);
    Task<int> RevokeActiveTokensByUserIdAsync(string userId, DateTime revokedDate, CancellationToken cancellationToken = default);
}