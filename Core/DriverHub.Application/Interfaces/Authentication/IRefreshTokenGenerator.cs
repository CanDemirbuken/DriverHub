using DriverHub.Application.Contracts.Authentication.Token;

namespace DriverHub.Application.Interfaces.Authentication;

public interface IRefreshTokenGenerator
{
    GeneratedRefreshToken Generate();
}