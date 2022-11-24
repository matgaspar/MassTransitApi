namespace MassTransitMQ.Domain.Interfaces.Services;

public interface IService<in TInput, TOutput, TKey>
{
    Task<IEnumerable<TOutput>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<TOutput> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<TKey> AddAsync(TInput input, CancellationToken cancellationToken = default);
    Task UpdateAsync(TKey id, TInput input, CancellationToken cancellationToken = default);
    Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}