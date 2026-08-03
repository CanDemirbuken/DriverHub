using DriverHub.Application.Interfaces.Authentication.Token.Refresh;
using System.Security.Cryptography;
using System.Text;

namespace DriverHub.Infrastructure.Services.Authentication.Token.Refresh;

public sealed class RefreshTokenHasher : IRefreshTokenHasher
{
    public string Hash(string refreshToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(refreshToken);

        byte[] tokenBytes = Encoding.UTF8.GetBytes(refreshToken);
        byte[] hashBytes = SHA256.HashData(tokenBytes);

        return Convert.ToHexString(hashBytes);
    }
}