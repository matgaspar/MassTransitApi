namespace MassTransitMQ.Domain.Interfaces.Data.Repositories;

public interface IRepository<TEntity, TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<TKey> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TKey id, TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}