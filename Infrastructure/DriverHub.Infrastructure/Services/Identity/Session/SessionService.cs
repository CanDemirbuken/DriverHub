using DriverHub.Application.Common.Errors.Identity;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Identity.Session;
using DriverHub.Application.Contracts.Identity.Token.AccessToken;
using DriverHub.Application.Contracts.Identity.Token.RefreshToken;
using DriverHub.Application.Interfaces.Authentication;
using DriverHub.Application.Interfaces.Authentication.Token.Access;
using DriverHub.Application.Interfaces.Authentication.Token.Refresh;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace DriverHub.Infrastructure.Services.Identity.Session;

public sealed class SessionService(IRefreshTokenHasher refreshTokenHasher, IRefreshTokenRepository refreshTokenRepository, UserManager<AppUser> userManager, IJwtTokenService jwtTokenService, IRefreshTokenGenerator refreshTokenGenerator, IUnitOfWork unitOfWork) : ISessionService
{
    public async Task<Result<SessionResponse>> CreateSessionAsync(CreateSessionRequest request, CancellationToken cancellationToken = default)
    {
        var accessTokenRequest = new CreateAccessTokenRequest(
            request.UserId,
            request.Email,
            request.Roles.ToList());

        GeneratedAccessToken accessToken = jwtTokenService.Generate(accessTokenRequest);
        GeneratedRefreshToken refreshToken = refreshTokenGenerator.Generate();

        string refreshTokenHash = refreshTokenHasher.Hash(refreshToken.Token);
        DateTime createdDate = DateTime.UtcNow;

        var storeRefreshTokenRequest = new StoreRefreshTokenRequest(
            refreshTokenHash,
            createdDate,
            refreshToken.ExpiresAt,
            request.UserId);

        await refreshTokenRepository.CreateAsync(storeRefreshTokenRequest, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new SessionResponse(
            accessToken.Token,
            accessToken.ExpiresAt,
            refreshToken.Token,
            refreshToken.ExpiresAt);

        return Result<SessionResponse>.Success(response);
    }

    public async Task<Result<RefreshSessionResponse>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        string currentTokenHash = refreshTokenHasher.Hash(refreshToken);
        DateTime currentDate = DateTime.UtcNow;

        RefreshTokenRecord? currentToken = await refreshTokenRepository.GetByHashAsync(currentTokenHash, cancellationToken);

        if (currentToken is null)
            return InvalidRefreshTokenResult();

        AppUser? user = await userManager.FindByIdAsync(currentToken.UserId);

        if (user is null || !user.IsActive || user.IsDeleted)
            return Result<RefreshSessionResponse>.Failure(AuthenticationErrors.InvalidUser);

        if (currentToken.IsReuseDetected())
        {
            await refreshTokenRepository.RevokeActiveTokensByUserIdAsync(currentToken.UserId, currentDate, cancellationToken);
            return ReusedRefreshTokenResult();
        }

        if (!currentToken.IsActive(currentDate))
            return InvalidRefreshTokenResult();

        IList<string> roles = await userManager.GetRolesAsync(user);

        var accessTokenRequest = new CreateAccessTokenRequest(
            user.Id,
            user.Email!,
            roles.ToList());

        GeneratedAccessToken accessToken = jwtTokenService.Generate(accessTokenRequest);
        GeneratedRefreshToken newRefreshToken = refreshTokenGenerator.Generate();

        string newRefreshTokenHash = refreshTokenHasher.Hash(newRefreshToken.Token);

        var storeRefreshTokenRequest = new StoreRefreshTokenRequest(
            newRefreshTokenHash,
            currentDate,
            newRefreshToken.ExpiresAt,
            user.Id);

        bool rotationSucceeded;

        await using (IUnitOfWorkTransaction transaction =
            await unitOfWork.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                rotationSucceeded = await refreshTokenRepository.RotateAsync(
                    currentToken.Id,
                    storeRefreshTokenRequest,
                    currentDate,
                    cancellationToken);

                if (rotationSucceeded)
                {
                    await unitOfWork.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                else
                {
                    await transaction.RollbackAsync(CancellationToken.None);
                }
            }
            catch
            {
                await transaction.RollbackAsync(CancellationToken.None);
                throw;
            }
        }

        if (!rotationSucceeded)
        {
            RefreshTokenRecord? refreshedCurrentToken = await refreshTokenRepository.GetByHashAsync(
                currentTokenHash,
                cancellationToken);

            if (refreshedCurrentToken?.IsReuseDetected() == true)
            {
                await refreshTokenRepository.RevokeActiveTokensByUserIdAsync(
                    refreshedCurrentToken.UserId,
                    DateTime.UtcNow,
                    cancellationToken);
            }

            return ReusedRefreshTokenResult();
        }

        var response = new RefreshSessionResponse(
            accessToken.Token,
            accessToken.ExpiresAt,
            newRefreshToken.Token,
            newRefreshToken.ExpiresAt);

        return Result<RefreshSessionResponse>.Success(response);
    }

    public async Task<Result> RevokeAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        string refreshTokenHash = refreshTokenHasher.Hash(refreshToken);
        DateTime revokedDate = DateTime.UtcNow;

        bool revoked = await refreshTokenRepository.RevokeAsync(refreshTokenHash, revokedDate, cancellationToken);
        if (!revoked)
            return Result.Failure(SessionErrors.InvalidRefreshToken);

        return Result.Success();
    }

    public async Task<Result> RevokeAllAsync(string userId, CancellationToken cancellationToken = default)
    {
        await refreshTokenRepository.RevokeActiveTokensByUserIdAsync(userId, DateTime.UtcNow, cancellationToken);
        return Result.Success();
    }

    private static Result<RefreshSessionResponse> InvalidRefreshTokenResult()
        => Result<RefreshSessionResponse>.Failure(SessionErrors.InvalidRefreshToken);

    private static Result<RefreshSessionResponse> ReusedRefreshTokenResult()
        => Result<RefreshSessionResponse>.Failure(SessionErrors.ReusedRefreshToken);
}
