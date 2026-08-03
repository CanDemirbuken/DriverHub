using DriverHub.Application.Contracts.Authentication.Token.AccessToken;

namespace DriverHub.Application.Interfaces.Authentication.Token.Access;

public interface IJwtTokenService
{
    GeneratedAccessToken Generate(CreateAccessTokenRequest request);
}