using DriverHub.Application.Contracts.Authentication.Token.RefreshToken;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Persistence.Context;
using DriverHub.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<RefreshTokenRecord?> GetByHashAsync(string tokenHash, CancellationToken cancellationToken = default)
    {
        return await context.Set<RefreshToken>()
            .AsNoTracking()
            .Where(token => token.TokenHash == tokenHash)
            .Select(token => new RefreshTokenRecord(
                token.Id,
                token.UserId,
                token.ExpiresDate,
                token.RevokedDate))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> RotateAsync(int currentTokenId, StoreRefreshTokenRequest newToken, DateTime revokedDate, CancellationToken cancellationToken = default)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        int affectedRows = await context.Set<RefreshToken>()
            .Where(token =>
                token.Id == currentTokenId &&
                token.RevokedDate == null &&
                token.ExpiresDate > revokedDate)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(token => token.RevokedDate, revokedDate)
                .SetProperty(token => token.ReplacedByTokenHash, newToken.TokenHash),
                cancellationToken);

        if (affectedRows == 0)
        {
            await transaction.RollbackAsync(cancellationToken);
            return false;
        }

        var refreshToken = new RefreshToken
        {
            TokenHash = newToken.TokenHash,
            CreatedDate = newToken.CreatedDate,
            ExpiresDate = newToken.ExpiresDate,
            UserId = newToken.UserId
        };

        await context.Set<RefreshToken>().AddAsync(refreshToken, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return true;
    }
}