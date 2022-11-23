using MassTransitMQ.Infra.Data.Context;
using MassTransitMQ.Infra.Data.UoW.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task BeginTransaction()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            _dbContext.SavingChanges += (sender, args) 
                => Console.WriteLine("Saving data in database...");

            _dbContext.SavedChanges += (sender, args) 
                => Console.WriteLine("{0} entities successfully recorded!", args.EntitiesSavedCount);

            _dbContext.SaveChangesFailed += (sender, args) 
                => Console.WriteLine("An error occurred while trying to process the request on the database: {0}", args.Exception.Message);

            await _dbContext.SaveChangesAsync();

            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }
    }
}
