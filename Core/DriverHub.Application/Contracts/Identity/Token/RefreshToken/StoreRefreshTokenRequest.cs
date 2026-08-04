namespace DriverHub.Application.Contracts.Identity.Token.RefreshToken;

public sealed record StoreRefreshTokenRequest(string TokenHash, DateTime CreatedDate, DateTime ExpiresDate, string UserId);