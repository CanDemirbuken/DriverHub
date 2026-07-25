namespace DriverHub.Application.Interfaces.Authentication;

public interface IRefreshTokenHasher
{
    string Hash(string refreshToken);
}