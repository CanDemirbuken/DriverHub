using DriverHub.Application.Common.Constants;
using DriverHub.Application.Common.Errors;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication.Login;
using DriverHub.Application.Contracts.Authentication.Register;
using DriverHub.Application.Contracts.Authentication.Token;
using DriverHub.Application.Contracts.Authentication.Token.AccessToken;
using DriverHub.Application.Contracts.Authentication.Token.RefreshToken;
using DriverHub.Application.Interfaces.Authentication;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Application.Interfaces.UnitOfWork;
using DriverHub.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace DriverHub.Infrastructure.Services.Authentication;

public sealed class AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtTokenService jwtTokenService, IRefreshTokenGenerator refreshTokenGenerator, IRefreshTokenHasher refreshTokenHasher, IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork) : IAuthenticationService
{
    public async Task<Result<LoginUserResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken = default)
    {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);

        if (user is null || !user.IsActive || user.IsDeleted)
            return Result<LoginUserResponse>.Failure(AuthenticationErrors.InvalidCredentials);

        SignInResult loginResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

        if (loginResult.IsLockedOut)
            return Result<LoginUserResponse>.Failure(AuthenticationErrors.UserLocked);

        if (!loginResult.Succeeded)
            return Result<LoginUserResponse>.Failure(AuthenticationErrors.InvalidCredentials);

        IList<string> roles = await userManager.GetRolesAsync(user);

        var accessTokenRequest = new CreateAccessTokenRequest(
            user.Id,
            user.Email!,
            roles.ToList());

        GeneratedAccessToken accessToken = jwtTokenService.Generate(accessTokenRequest);
        GeneratedRefreshToken refreshToken = refreshTokenGenerator.Generate();

        string refreshTokenHash = refreshTokenHasher.Hash(refreshToken.Token);
        DateTime createdDate = DateTime.UtcNow;

        var storeRefreshTokenRequest = new StoreRefreshTokenRequest(
            refreshTokenHash,
            createdDate,
            refreshToken.ExpiresAt,
            user.Id);

        await refreshTokenRepository.CreateAsync(storeRefreshTokenRequest, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new LoginUserResponse(
            accessToken.Token,
            accessToken.ExpiresAt,
            refreshToken.Token,
            refreshToken.ExpiresAt);

        return Result<LoginUserResponse>.Success(response);
    }

    public async Task<Result<RegisterUserResponse>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        AppUser? existingUser = await userManager.FindByEmailAsync(request.Email);

        if (existingUser is not null)
            return Result<RegisterUserResponse>.Failure(AuthenticationErrors.EmailAlreadyExists);

        var user = new AppUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
            IsActive = true,
            IsDeleted = false
        };

        IdentityResult creationResult = await userManager.CreateAsync(user, request.Password);

        if (!creationResult.Succeeded)
        {
            IReadOnlyCollection<Error> errors = IdentityErrorMapper
                .Map(creationResult.Errors)
                .Select(message => Error.Validation(
                    "Identity.Validation",
                    message))
                .ToArray();

            return Result<RegisterUserResponse>.Failure(errors);
        }

        IdentityResult roleResult = await userManager.AddToRoleAsync(user, RoleNames.User);

        if (!roleResult.Succeeded)
        {
            IdentityResult deletionResult = await userManager.DeleteAsync(user);

            if (!deletionResult.Succeeded)
            {
                throw new InvalidOperationException($"Kullanıcı oluşturuldu ancak '{RoleNames.User}' rolü atanamadı ve kullanıcı kaydı geri alınamadı.");
            }

            IReadOnlyCollection<Error> errors = IdentityErrorMapper
                .Map(roleResult.Errors)
                .Select(message => Error.Failure(message))
                .ToArray();

            return Result<RegisterUserResponse>.Failure(
                errors.Count > 0
                    ? errors
                    : [AuthenticationErrors.DefaultRoleAssignmentFailed]);
        }

        var response = new RegisterUserResponse(user.Id);

        return Result<RegisterUserResponse>.Success(response);
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

    private static Result<RefreshSessionResponse> InvalidRefreshTokenResult()
        => Result<RefreshSessionResponse>.Failure(AuthenticationErrors.InvalidRefreshToken);

    private static Result<RefreshSessionResponse> ReusedRefreshTokenResult()
        => Result<RefreshSessionResponse>.Failure(AuthenticationErrors.ReusedRefreshToken);
}