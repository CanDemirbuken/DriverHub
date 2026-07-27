using DriverHub.Application.Contracts.Authentication.Token.AccessToken;

namespace DriverHub.Application.Interfaces.Authentication;

public interface IJwtTokenService
{
    GeneratedAccessToken Generate(CreateAccessTokenRequest request);
}