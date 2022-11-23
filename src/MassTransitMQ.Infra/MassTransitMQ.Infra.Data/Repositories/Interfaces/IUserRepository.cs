using MassTransitMQ.Domain.Entities;

namespace MassTransitMQ.Infra.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<User> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> CreateAsync(User user, CancellationToken cancellationToken = default);
        Task UpdateAsync(User user, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid user, CancellationToken cancellationToken = default);
    }
}
