using DriverHub.Application.Contracts.Authentication.Token;

namespace DriverHub.Application.Interfaces.Authentication;

public interface IJwtTokenService
{
    TokenResponse GenerateToken(CreateTokenRequest request);
}
