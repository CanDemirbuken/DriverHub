using DriverHub.Application.Common.Constants;
using DriverHub.Application.Common.Results;
using DriverHub.Application.Contracts.Authentication;
using DriverHub.Application.Contracts.Authentication.Login;
using DriverHub.Application.Contracts.Authentication.Register;
using DriverHub.Application.Contracts.Authentication.Token;
using DriverHub.Application.Interfaces.Authentication;
using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Persistence.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DriverHub.Infrastructure.Services.Authentication;

public sealed class AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtTokenService tokenService, IRefreshTokenHasher refreshTokenHasher, IRefreshTokenRepository refreshTokenRepository) : IAuthenticationService
{
    public async Task<Result<LoginUserResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken = default)
    {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result<LoginUserResponse>.Failure(StatusCodes.Status401Unauthorized, "E-mail ya da şifre hatalı.");

        if (!user.IsActive || user.IsDeleted)
            return Result<LoginUserResponse>.Failure(StatusCodes.Status401Unauthorized, "E-mail ya da şifre hatalı.");

        SignInResult loginResult =
            await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

        if (loginResult.IsLockedOut)
            return Result<LoginUserResponse>.Failure(StatusCodes.Status423Locked, "Kullanıcı hesabı başarısız giriş denemeleri nedeniyle geçici olarak kilitlenmiştir.");

        if (!loginResult.Succeeded)
            return Result<LoginUserResponse>.Failure(StatusCodes.Status401Unauthorized, "E-mail ya da şifre hatalı.");

        IList<string> roles = await userManager.GetRolesAsync(user);

        var createTokenRequest = new CreateTokenRequest(
            user.Id,
            user.Email!,
            roles.ToList());

        TokenResponse tokenResponse = tokenService.GenerateToken(createTokenRequest);

        string refreshTokenHash = refreshTokenHasher.Hash(tokenResponse.RefreshToken);

        var storeRefreshTokenRequest = new StoreRefreshTokenRequest(
            refreshTokenHash,
            DateTime.UtcNow,
            tokenResponse.RefreshTokenExpiresAt,
            user.Id);

        await refreshTokenRepository.CreateAsync(storeRefreshTokenRequest, cancellationToken);

        var response = new LoginUserResponse(
            tokenResponse.AccessToken,
            tokenResponse.AccessTokenExpiresAt,
            tokenResponse.RefreshToken,
            tokenResponse.RefreshTokenExpiresAt);

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
                throw new InvalidOperationException($"Kullanıcı oluşturuldu ancak '{RoleNames.User}' rolü " + "atanamadı ve kullanıcı kaydı geri alınamadı.");

            IReadOnlyCollection<string> errors = IdentityErrorMapper.Map(roleResult.Errors);

            return Result<RegisterUserResponse>.Failure(StatusCodes.Status500InternalServerError,
                errors.Count > 0
                    ? errors
                    : ["Kullanıcıya varsayılan rol atanamadı."]);
        }

        var response = new RegisterUserResponse(user.Id);

        return Result<RegisterUserResponse>.Success(response, StatusCodes.Status201Created);
    }
}