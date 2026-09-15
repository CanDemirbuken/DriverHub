using DriverHub.Application.Interfaces.Repositories;
using DriverHub.Domain.Entities;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.Repositories;

public sealed class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    private readonly AppDbContext context;

    public ReservationRepository(AppDbContext context) : base(context) => this.context = context;

    public Task<Car?> GetCarForUpdateAsync(Guid carId, CancellationToken cancellationToken = default)
    {
        if (context.Database.CurrentTransaction is null)
            throw new InvalidOperationException("Reservation writes require a transaction.");

        // A car row is the common lock even when no reservation exists yet. HOLDLOCK keeps
        // the lock through commit; UPDLOCK avoids concurrent readers upgrading together.
        return context.Set<Car>()
            .FromSqlInterpolated($"SELECT * FROM [Cars] WITH (UPDLOCK, HOLDLOCK) WHERE [Id] = {carId}")
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);
    }
}
