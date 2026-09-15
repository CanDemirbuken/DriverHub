namespace DriverHub.Application.Features.Entities.Reservations.Common;

public static class ReservationTimePolicy
{
    public static int GetRentalDays(DateTime startDate, DateTime endDate) =>
        Math.Max(1, (int)Math.Ceiling((endDate - startDate).TotalDays));

    // Keep the Angular reservation-time-policy constant in sync.
    public const int StartGracePeriodMinutes = 5;

    public static bool IsStartAllowed(DateTime startDate) => IsStartAllowed(startDate, DateTime.UtcNow);

    public static bool IsStartAllowed(DateTime startDate, DateTime utcNow)
    {
        DateTime currentMinute = utcNow.AddTicks(-(utcNow.Ticks % TimeSpan.TicksPerMinute));
        return startDate >= currentMinute.AddMinutes(-StartGracePeriodMinutes);
    }
}
