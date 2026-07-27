using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverHub.Persistence.Identity.Configurations;

public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(refreshToken => refreshToken.Id);

        builder.Property(refreshToken => refreshToken.TokenHash)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(refreshToken => refreshToken.CreatedDate)
            .IsRequired();

        builder.Property(refreshToken => refreshToken.ExpiresDate)
            .IsRequired();

        builder.Property(refreshToken => refreshToken.RevokedDate)
            .IsRequired(false);

        builder.Property(refreshToken => refreshToken.ReplacedByTokenHash)
            .HasMaxLength(64)
            .IsRequired(false);

        builder.Property(refreshToken => refreshToken.UserId)
            .IsRequired();

        builder.HasIndex(refreshToken => refreshToken.TokenHash)
            .IsUnique();

        builder.HasIndex(refreshToken => refreshToken.UserId);
    }
}