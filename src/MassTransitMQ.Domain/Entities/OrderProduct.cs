namespace MassTransitMQ.Domain.Entities
{
    public class OrderProduct
    {
        public OrderProduct(Guid orderId, Guid productId, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }

        public OrderProduct(Guid orderId, Guid productId, int quantity, DateTime createdOn, DateTime updatedOn)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
        }

        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        public virtual Order? Order { get; private set; }
        public virtual Product? Product { get; private set; }
    }
}