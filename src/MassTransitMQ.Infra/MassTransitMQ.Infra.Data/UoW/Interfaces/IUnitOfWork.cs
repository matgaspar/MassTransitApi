namespace MassTransitMQ.Infra.Data.UoW.Interfaces
{
    public interface IUnitOfWork
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}
