namespace MassTransitMQ.Domain.Interfaces.Data;

public interface IUnitOfWork
{
    Task BeginTransaction();
    Task Commit();
    Task Rollback();
}