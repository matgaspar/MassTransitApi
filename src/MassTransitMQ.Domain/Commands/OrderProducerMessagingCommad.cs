using MassTransitMQ.Domain.Commands.Interfaces;

namespace MassTransitMQ.Domain.Commands
{
    public class OrderProducerMessagingCommad : OrderMessaging, ICommand
    {
        public OrderProducerMessagingCommad(string number, string buyerName, string address)
            : base(number, buyerName, address) {}

        public Task Action()
        {
            throw new NotImplementedException();
        }
    }
}