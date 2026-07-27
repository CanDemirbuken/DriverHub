using DriverHub.Application.Common.Constants;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication.Login;
using DriverHub.Application.Contracts.Authentication.Register;
using DriverHub.Application.Contracts.Authentication.Token;
using DriverHub.Application.Contracts.Authentication.Token.AccessToken;
using DriverHub.Application.Contracts.Authentication.Token.RefreshToken;
using DriverHub.Application.Interfaces.Authentication;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Persistence.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DriverHub.Infrastructure.Services.Authentication;

public sealed class AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtTokenService jwtTokenService, IRefreshTokenGenerator refreshTokenGenerator, IRefreshTokenHasher refreshTokenHasher, IRefreshTokenRepository refreshTokenRepository) : IAuthenticationService
{
    public async Task<Result<LoginUserResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken = default)
    {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);

        if (user is null || !user.IsActive || user.IsDeleted)
            return Result<LoginUserResponse>.Failure(StatusCodes.Status401Unauthorized, "E-mail ya da şifre hatalı.");

        SignInResult loginResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

        if (loginResult.IsLockedOut)
            return Result<LoginUserResponse>.Failure(StatusCodes.Status423Locked, "Kullanıcı hesabı başarısız giriş denemeleri nedeniyle geçici olarak kilitlenmiştir.");

        if (!loginResult.Succeeded)
            return Result<LoginUserResponse>.Failure(StatusCodes.Status401Unauthorized, "E-mail ya da şifre hatalı.");

        IList<string> roles = await userManager.GetRolesAsync(user);

        var accessTokenRequest = new CreateAccessTokenRequest(user.Id, user.Email!, roles.ToList());

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

        var response = new LoginUserResponse(
            accessToken.Token,
            accessToken.ExpiresAt,
            refreshToken.Token,
            refreshToken.ExpiresAt);

        return Result<LoginUserResponse>.Success(response, StatusCodes.Status200OK);
    }

    public async Task<Result<RegisterUserResponse>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        AppUser? existingUser = await userManager.FindByEmailAsync(request.Email);

        if (existingUser is not null)
            return Result<RegisterUserResponse>.Failure(StatusCodes.Status409Conflict, "Bu e-mail adresi ile kayıtlı bir kullanıcı bulunmaktadır.");

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
            IReadOnlyCollection<string> errors = IdentityErrorMapper.Map(creationResult.Errors);
            return Result<RegisterUserResponse>.Failure(StatusCodes.Status400BadRequest, errors);
        }

        IdentityResult roleResult = await userManager.AddToRoleAsync(user, RoleNames.User);

        if (!roleResult.Succeeded)
        {
            IdentityResult deletionResult = await userManager.DeleteAsync(user);

            if (!deletionResult.Succeeded)
                throw new InvalidOperationException($"Kullanıcı oluşturuldu ancak '{RoleNames.User}' rolü atanamadı ve kullanıcı kaydı geri alınamadı.");

            IReadOnlyCollection<string> errors = IdentityErrorMapper.Map(roleResult.Errors);

            return Result<RegisterUserResponse>.Failure(
                StatusCodes.Status500InternalServerError,
                errors.Count > 0
                    ? errors
                    : ["Kullanıcıya varsayılan rol atanamadı."]);
        }

        var response = new RegisterUserResponse(user.Id);

        return Result<RegisterUserResponse>.Success(response, StatusCodes.Status201Created);
    }

    public async Task<Result<RefreshSessionResponse>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        string currentTokenHash = refreshTokenHasher.Hash(refreshToken);

        RefreshTokenRecord? currentToken = await refreshTokenRepository.GetByHashAsync(currentTokenHash, cancellationToken);

        if (currentToken is null)
            return Result<RefreshSessionResponse>.Failure(StatusCodes.Status401Unauthorized, "Refresh token geçersiz veya kullanım süresi dolmuş.");

        DateTime currentDate = DateTime.UtcNow;

        if (!currentToken.IsActive(currentDate))
            return Result<RefreshSessionResponse>.Failure(StatusCodes.Status401Unauthorized, "Refresh token geçersiz veya kullanım süresi dolmuş.");

        AppUser? user = await userManager.FindByIdAsync(currentToken.UserId);

        if (user is null || !user.IsActive || user.IsDeleted)
            return Result<RefreshSessionResponse>.Failure(StatusCodes.Status401Unauthorized, "Kullanıcı bilgisi geçersiz.");

        IList<string> roles = await userManager.GetRolesAsync(user);

        var accessTokenRequest = new CreateAccessTokenRequest(user.Id, user.Email!, roles.ToList());

        GeneratedAccessToken accessToken = jwtTokenService.Generate(accessTokenRequest);
        GeneratedRefreshToken newRefreshToken = refreshTokenGenerator.Generate();

        string newRefreshTokenHash = refreshTokenHasher.Hash(newRefreshToken.Token);

        var storeRefreshTokenRequest = new StoreRefreshTokenRequest(
            newRefreshTokenHash,
            currentDate,
            newRefreshToken.ExpiresAt,
            user.Id);

        bool rotationSucceeded = await refreshTokenRepository.RotateAsync(
            currentToken.Id,
            storeRefreshTokenRequest,
            currentDate,
            cancellationToken);

        if (!rotationSucceeded)
            return Result<RefreshSessionResponse>.Failure(StatusCodes.Status401Unauthorized, "Refresh token daha önce kullanılmış veya geçersiz hale getirilmiştir.");

        var response = new RefreshSessionResponse(
            accessToken.Token,
            accessToken.ExpiresAt,
            newRefreshToken.Token,
            newRefreshToken.ExpiresAt);

        return Result<RefreshSessionResponse>.Success(response, StatusCodes.Status200OK);
    }
}