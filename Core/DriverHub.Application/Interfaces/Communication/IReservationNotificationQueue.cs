using DriverHub.Domain.Entities;

namespace DriverHub.Application.Interfaces.Communication;

public interface IReservationNotificationQueue
{
    // Enlists the message in the caller's Unit of Work; never sends SMTP inside a reservation transaction.
    Task EnqueueAsync(Reservation reservation, string vehicle, string pickupLocation, CancellationToken cancellationToken = default);
}
