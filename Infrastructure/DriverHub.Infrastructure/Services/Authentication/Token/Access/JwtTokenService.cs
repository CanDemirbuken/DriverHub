using DriverHub.Application.Contracts.Authentication.Token.AccessToken;
using DriverHub.Application.Interfaces.Authentication.Token.Access;
using DriverHub.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DriverHub.Infrastructure.Services.Authentication.Token.Access;

public sealed class JwtTokenService(IOptions<JwtOptions> jwtOptions) : IJwtTokenService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public GeneratedAccessToken Generate(CreateAccessTokenRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        DateTime createdAt = DateTime.UtcNow;
        DateTime expiresAt = createdAt.AddMinutes(_jwtOptions.ExpirationMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, request.UserId),
            new(ClaimTypes.NameIdentifier, request.UserId),
            new(ClaimTypes.Email, request.Email),
            new(JwtRegisteredClaimNames.UniqueName, request.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
        };

        claims.AddRange(request.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: createdAt,
            expires: expiresAt,
            signingCredentials: signingCredentials);

        string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return new GeneratedAccessToken(token, expiresAt);
    }
}