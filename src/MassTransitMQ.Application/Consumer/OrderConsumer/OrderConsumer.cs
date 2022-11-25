using MassTransit;
using MassTransitMQ.Application.Commands.Order.CreateCommand;
using MassTransitMQ.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace MassTransitMQ.Application.Consumer.OrderConsumer
{
    public class OrderConsumer : IConsumer<CreateOrderCommand>
    {
        private readonly ILogger<OrderConsumer> _logger;
        private readonly IOrderService _orderService;

        public OrderConsumer(ILogger<OrderConsumer> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public async Task Consume(ConsumeContext<CreateOrderCommand> context)
        {
            var id = await _orderService.AddAsync(context.Message);

            _logger.LogInformation("Order [{id}] created!", id);

            await context.ConsumeCompleted;
        }
    }
}