using DriverHub.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DriverHub.Persistence.Extensions;

public static class ChangeTrackerExtensions
{
    public static void ApplyAuditRules(this ChangeTracker changeTracker)
    {
        var utcNow = DateTime.UtcNow;

        var entries = changeTracker
            .Entries<Entity>()
            .Where(entry =>
                entry.State == EntityState.Added ||
                entry.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            ApplyAuditRule(entry, utcNow);
        }
    }

    private static void ApplyAuditRule(EntityEntry<Entity> entry, DateTime utcNow)
    {
        switch (entry.State)
        {
            case EntityState.Added:
                entry.Entity.CreatedDate = utcNow;
                break;

            case EntityState.Modified:
                entry.Entity.UpdatedDate = utcNow;
                entry.Property(entity => entity.CreatedDate).IsModified = false;
                break;
        }
    }
}