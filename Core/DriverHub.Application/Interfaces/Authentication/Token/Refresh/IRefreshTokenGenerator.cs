using DriverHub.Application.Contracts.Identity.Token.RefreshToken;

namespace DriverHub.Application.Interfaces.Authentication.Token.Refresh;

public interface IRefreshTokenGenerator
{
    GeneratedRefreshToken Generate();
}