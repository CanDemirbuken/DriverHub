using System.Linq.Expressions;

namespace DriverHub.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> WhereAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        T entity,
        CancellationToken cancellationToken = default);

    Task AddRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default);

    void Update(T entity);

    void UpdateRange(IEnumerable<T> entities);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);
}