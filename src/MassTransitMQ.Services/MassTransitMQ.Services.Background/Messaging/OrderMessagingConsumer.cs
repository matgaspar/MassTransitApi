using MassTransit;
using MassTransitMQ.Domain.Messaging.Models;
using Microsoft.Extensions.Logging;

namespace MassTransitMQ.Services.Background.Messaging
{
    public class OrderMessagingConsumer : IConsumer<OrderMessagingModel>
    {
        private readonly ILogger<OrderMessagingConsumer> _logger;

        public OrderMessagingConsumer(ILogger<OrderMessagingConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderMessagingModel> context)
        {
            _logger.LogInformation("[{MessageId}] {CreatedOn} - Order: {Number} {BuyerName} | {Address}", 
                context.MessageId,
                context.Message.Number, 
                context.Message.BuyerName, 
                context.Message.Address, 
                context.Message.CreatedOn
            );

            return Task.CompletedTask;
        }
    }
}