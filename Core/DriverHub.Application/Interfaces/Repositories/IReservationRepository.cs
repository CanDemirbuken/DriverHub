using DriverHub.Domain.Entities;

namespace DriverHub.Application.Interfaces.Repositories;

public interface IReservationRepository : IRepository<Reservation>
{
    // Requires an active Unit of Work transaction. Serializes reservation writes per physical car.
    Task<Car?> GetCarForUpdateAsync(Guid carId, CancellationToken cancellationToken = default);
}
