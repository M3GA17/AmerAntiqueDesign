using Shared.Primitives;

namespace Shared.Base.Interfaces;

// Add TIdUser as a generic type parameter to the interface
public interface IBaseRepository<T, in TId>
    where T : AggregateRoot<TId>
    where TId : ValueObject
{
    Task<T?> GetAsync(TId id, CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetListAsync(CancellationToken cancellationToken);
    Task RemoveAsync(TId id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken);
}
