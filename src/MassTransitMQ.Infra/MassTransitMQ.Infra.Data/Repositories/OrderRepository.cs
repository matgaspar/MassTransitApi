using MassTransitMQ.Domain.Entities;
using MassTransitMQ.Domain.Interfaces.Data;
using MassTransitMQ.Domain.Interfaces.Data.Repositories;
using MassTransitMQ.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MassTransitMQ.Infra.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUnitOfWork _uow;

    public OrderRepository(ApplicationDbContext dbContext, IUnitOfWork uow)
    {
        _dbContext = dbContext;
        _uow = uow;
    }

    public async Task<IEnumerable<Order>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var skip = (page - 1) * pageSize;
        return await _dbContext.Orders
            .AsNoTracking()
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid> AddAsync(Order entity, CancellationToken cancellationToken = default)
    {
        var data = await _dbContext.Orders.AddAsync(entity, cancellationToken);
        
        await _uow.Commit();

        return data.Entity.Id;
    }

    public Task UpdateAsync(Guid id, Order entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var data = await _dbContext.Orders.FindAsync(id);

        if (data == null)
            throw new Exception($"Nenhum dado encontrado!");

        _dbContext.Orders.Remove(data);
    }
}