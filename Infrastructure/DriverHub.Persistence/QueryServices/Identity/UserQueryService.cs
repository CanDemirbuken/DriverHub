using DriverHub.Application.Interfaces.QueryServices.Identity;
using DriverHub.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DriverHub.Persistence.QueryServices.Identity;

public sealed class UserQueryService(AppDbContext context) : IUserQueryService
{
    public Task<ReservationCustomer?> GetReservationCustomerAsync(string userId, CancellationToken cancellationToken = default) =>
        context.Users.AsNoTracking()
            .Where(user => user.Id == userId && user.IsActive && !user.IsDeleted)
            .Select(user => new ReservationCustomer(user.FirstName, user.LastName, user.Email ?? "", user.PhoneNumber))
            .SingleOrDefaultAsync(cancellationToken);
}
