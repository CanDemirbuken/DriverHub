namespace DriverHub.Application.Contracts.Authentication.Token.RefreshToken;

public sealed record RefreshTokenRecord(int Id, string UserId, DateTime ExpiresDate, DateTime? RevokedDate, string? ReplacedByTokenHash)
{
    public bool IsActive(DateTime currentDate)
        => RevokedDate is null && currentDate < ExpiresDate;

    public bool IsReuseDetected()
        => RevokedDate is not null &&
           !string.IsNullOrWhiteSpace(ReplacedByTokenHash);
}