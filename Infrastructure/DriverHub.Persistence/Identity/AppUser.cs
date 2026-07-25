using Microsoft.AspNetCore.Identity;

namespace DriverHub.Persistence.Identity;

public sealed class AppUser : IdentityUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public bool IsActive { get; set; } = false;
    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedDate { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}