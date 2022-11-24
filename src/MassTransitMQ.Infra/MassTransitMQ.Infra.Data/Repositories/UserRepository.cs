using MassTransitMQ.Domain.Entities;
using MassTransitMQ.Domain.Interfaces.Data.Repositories;
using MassTransitMQ.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MassTransitMQ.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var skip = (page - 1) * pageSize;
            return await _dbContext.Users
                .AsNoTracking()
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<User> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Guid> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            var newUser = await _dbContext.Users.AddAsync(user, cancellationToken);
            
            return newUser.Entity.Id;
        }

        public async Task UpdateAsync(Guid id, User user, CancellationToken cancellationToken = default)
        {
            var r = await _dbContext.Users.ExecuteUpdateAsync(call => call.SetProperty(u => u, (ve) => user),
                cancellationToken
            );
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
                throw new Exception($"Nenhum dado encontrado!");

            _dbContext.Users.Remove(user);
        }
    }
}
