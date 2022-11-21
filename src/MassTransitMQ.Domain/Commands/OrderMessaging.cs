namespace MassTransitMQ.Domain.Commands
{
    public class OrderMessaging
    {
        public OrderMessaging(string? number, string? buyerName, string? address)
        {
            Number = number;
            BuyerName = buyerName;
            Address = address;
        }

        public string? Number { get; private set; }
        public string? BuyerName { get; private set; }
        public string? Address { get; private set; }
        
    }
}