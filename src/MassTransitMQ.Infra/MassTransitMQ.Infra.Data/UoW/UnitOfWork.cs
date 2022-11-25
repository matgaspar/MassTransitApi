using MassTransitMQ.Domain.Interfaces.Data;
using MassTransitMQ.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace MassTransitMQ.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private IDbContextTransaction _transaction;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task BeginTransaction()
        {
            // _transaction = await _dbContext.Database.BeginTransactionAsync();
            return Task.CompletedTask;
        }

        public async Task Commit()
        {
            _dbContext.SavingChanges += (sender, args) 
                => Console.WriteLine("Saving data in database...");

            _dbContext.SavedChanges += (sender, args)
                => Console.WriteLine("Entity successfully recorded!");

            _dbContext.SaveChangesFailed += (sender, args) 
                => Console.WriteLine("An error occurred while trying to process the request on the database");

            await _dbContext.SaveChangesAsync();

            //await _transaction.CommitAsync();
        }

        public Task Rollback()
        {
            // await _transaction.RollbackAsync();
            return Task.CompletedTask;
        }
    }
}
