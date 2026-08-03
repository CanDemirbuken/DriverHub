namespace DriverHub.Application.Interfaces.Authentication.Token.Refresh;

public interface IRefreshTokenHasher
{
    string Hash(string refreshToken);
}