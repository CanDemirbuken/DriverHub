using DriverHub.Application.Contracts.Authentication.Token;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Persistence.Context;
using DriverHub.Persistence.Identity;

namespace DriverHub.Persistence.Repositories;

public sealed class RefreshTokenRepository(AppDbContext context) : IRefreshTokenRepository
{
    public async Task CreateAsync(StoreRefreshTokenRequest request, CancellationToken cancellationToken = default)
    {
        var refreshToken = new RefreshToken
        {
            TokenHash = request.TokenHash,
            CreatedDate = request.CreatedDate,
            ExpiresDate = request.ExpiresDate,
            UserId = request.UserId
        };

        await context.Set<RefreshToken>().AddAsync(refreshToken, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}