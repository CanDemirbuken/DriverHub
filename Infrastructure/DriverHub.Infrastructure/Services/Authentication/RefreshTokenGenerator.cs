using DriverHub.Application.Contracts.Authentication.Token;
using DriverHub.Application.Interfaces.Authentication;
using DriverHub.Infrastructure.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace DriverHub.Infrastructure.Services.Authentication;

public sealed class RefreshTokenGenerator(IOptions<RefreshTokenOptions> refreshTokenOptions) : IRefreshTokenGenerator
{
    private readonly RefreshTokenOptions _refreshTokenOptions = refreshTokenOptions.Value;

    public GeneratedRefreshToken Generate()
    {
        string token = WebEncoders.Base64UrlEncode(RandomNumberGenerator.GetBytes(32));
        DateTime expiresAt = DateTime.UtcNow.AddDays(_refreshTokenOptions.ExpireDays);

        return new GeneratedRefreshToken(token, expiresAt);
    }
}