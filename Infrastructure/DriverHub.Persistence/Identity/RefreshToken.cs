namespace DriverHub.Persistence.Identity;

public sealed class RefreshToken
{
    public int Id { get; set; }

    public string TokenHash { get; set; } = default!;

    public DateTime CreatedDate { get; set; }
    public DateTime ExpiresDate { get; set; }
    public DateTime? RevokedDate { get; set; }

    public string? ReplacedByTokenHash { get; set; }

    public string UserId { get; set; } = default!;
    public AppUser User { get; set; } = default!;

    public bool IsExpired => DateTime.UtcNow >= ExpiresDate;
    public bool IsRevoked => RevokedDate.HasValue;
    public bool IsActive => !IsExpired && !IsRevoked;
}