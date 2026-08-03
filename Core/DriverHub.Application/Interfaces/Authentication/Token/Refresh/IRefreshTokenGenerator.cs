using DriverHub.Application.Contracts.Authentication.Token;

namespace DriverHub.Application.Interfaces.Authentication.Token.Refresh;

public interface IRefreshTokenGenerator
{
    GeneratedRefreshToken Generate();
}