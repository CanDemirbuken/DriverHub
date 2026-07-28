using DriverHub.Application.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;

namespace DriverHub.Persistence.Transactions;

public sealed class EfUnitOfWorkTransaction(IDbContextTransaction transaction) : IUnitOfWorkTransaction
{
    public Task CommitAsync(CancellationToken cancellationToken = default)
        => transaction.CommitAsync(cancellationToken);

    public Task RollbackAsync(CancellationToken cancellationToken = default)
        => transaction.RollbackAsync(cancellationToken);

    public ValueTask DisposeAsync()
        => transaction.DisposeAsync();
}