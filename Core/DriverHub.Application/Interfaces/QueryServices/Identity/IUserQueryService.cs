namespace DriverHub.Application.Interfaces.QueryServices.Identity;

public sealed record ReservationCustomer(string FirstName, string LastName, string Email, string? Phone);

public interface IUserQueryService
{
    Task<ReservationCustomer?> GetReservationCustomerAsync(string userId, CancellationToken cancellationToken = default);
}
