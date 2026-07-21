using DriverHub.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DriverHub.Persistence.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
                entry.Property(p => p.CreatedDate).CurrentValue = DateTime.UtcNow;

            if (entry.State == EntityState.Modified)
            {
                entry.Property(p => p.UpdatedDate).CurrentValue = DateTime.UtcNow;
                entry.Property(p => p.CreatedDate).IsModified = false;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}