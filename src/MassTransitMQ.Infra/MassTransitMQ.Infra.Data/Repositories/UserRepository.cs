using System.Linq.Expressions;
using MassTransitMQ.Domain.Entities;
using MassTransitMQ.Infra.Data.Context;
using MassTransitMQ.Infra.Data.Repositories.Interfaces;
using MassTransitMQ.Infra.Data.UoW.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace MassTransitMQ.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _uow;

        public UserRepository(ApplicationDbContext dbContext, IUnitOfWork uow)
        {
            _dbContext = dbContext;
            _uow = uow;
        }

        public async Task<IEnumerable<User>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var skip = (pageNumber - 1) * pageSize;
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
                .FirstOrDefaultAsync();
        }

        public async Task<Guid> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            try
            {
                await _uow.BeginTransaction();

                var newUser = await _dbContext.Users.AddAsync(user, cancellationToken);

                await _uow.Commit();

                return newUser.Entity.Id;
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                throw new Exception(ex.Message, ex?.InnerException);
            }
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            try
            {
                await _uow.BeginTransaction();
                
                var r = await _dbContext.Users.ExecuteUpdateAsync(call => call.SetProperty(u => u, (ve) => user),
                    cancellationToken
                );
                
                await _uow.Commit();
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                throw new Exception(ex.Message, ex?.InnerException);
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _uow.BeginTransaction();

                var user = await _dbContext.Users.FindAsync(id);

                if (user == null)
                    throw new Exception($"Nenhum usuário encontrado!");

                _dbContext.Users.Remove(user);

                await _uow.Commit();
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                throw new Exception(ex.Message, ex?.InnerException);
            }
        }
    }
}
