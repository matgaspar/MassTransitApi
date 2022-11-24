using MassTransitMQ.Domain.Entities;

namespace MassTransitMQ.Domain.Interfaces.Data.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
}