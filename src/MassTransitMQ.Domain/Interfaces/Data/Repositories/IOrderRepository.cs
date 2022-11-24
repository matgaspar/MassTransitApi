using MassTransitMQ.Domain.Entities;

namespace MassTransitMQ.Domain.Interfaces.Data.Repositories;

public interface IOrderRepository : IRepository<Order, Guid>
{
}