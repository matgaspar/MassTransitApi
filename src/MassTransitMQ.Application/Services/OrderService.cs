using AutoMapper;
using MassTransitMQ.Domain.DTOs.IO.Input;
using MassTransitMQ.Domain.DTOs.IO.Output;
using MassTransitMQ.Domain.Entities;
using MassTransitMQ.Domain.Interfaces.Data;
using MassTransitMQ.Domain.Interfaces.Data.Repositories;
using MassTransitMQ.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace MassTransitMQ.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderService> _logger;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, ILogger<OrderService> logger, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderOutput>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var users = await _orderRepository.GetAllAsync(page, pageSize, cancellationToken);

        var result = _mapper.Map<IEnumerable<OrderOutput>>(users);

        return result;
    }

    public async Task<OrderOutput> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var data = await _orderRepository.FindByIdAsync(id, cancellationToken);

        var result = _mapper.Map<OrderOutput>(data);

        return result;
    }

    public async Task<Guid> AddAsync(OrderInput input, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            var entity = _mapper.Map<Order>(input);

            var id = await _orderRepository.AddAsync(entity, cancellationToken);

            await _unitOfWork.Commit();

            return id;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception(ex.Message, ex?.InnerException);
        }
    }

    public Task UpdateAsync(Guid id, OrderInput input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            await _orderRepository.DeleteAsync(id, cancellationToken);

            await _unitOfWork.Commit();
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception(ex.Message, ex?.InnerException);
        }
    }
}