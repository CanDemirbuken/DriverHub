using DriverHub.Application.Contracts.Authentication.Token;
using DriverHub.Application.Interfaces.Authentication;
using DriverHub.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DriverHub.Infrastructure.Services.Identity;

public sealed class JwtTokenService(IOptions<JwtOptions> jwtOptions, IOptions<RefreshTokenOptions> refreshTokenOptions) : IJwtTokenService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly RefreshTokenOptions _refreshTokenOptions = refreshTokenOptions.Value;

    public TokenResponse GenerateToken(CreateTokenRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, request.UserId),
            new(ClaimTypes.NameIdentifier, request.UserId),
            new(ClaimTypes.Email, request.Email),
            new(JwtRegisteredClaimNames.UniqueName, request.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
        };

        claims.AddRange(request.Roles.Select(
            role => new Claim(ClaimTypes.Role, role)));

        DateTime createdAt = DateTime.UtcNow;

        DateTime accessTokenExpiresAt = createdAt.AddMinutes(_jwtOptions.ExpirationMinutes);

        DateTime refreshTokenExpiresAt = createdAt.AddDays(_refreshTokenOptions.ExpireDays);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: createdAt,
            expires: accessTokenExpiresAt,
            signingCredentials: signingCredentials);

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        return new TokenResponse(
            accessToken,
            accessTokenExpiresAt,
            refreshToken,
            refreshTokenExpiresAt);
    }
}