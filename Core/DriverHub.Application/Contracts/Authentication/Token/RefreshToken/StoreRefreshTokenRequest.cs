namespace DriverHub.Application.Contracts.Authentication.Token.RefreshToken;

public sealed record StoreRefreshTokenRequest(string TokenHash, DateTime CreatedDate, DateTime ExpiresDate, string UserId);