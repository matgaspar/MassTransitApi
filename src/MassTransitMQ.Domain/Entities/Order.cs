using System.ComponentModel.DataAnnotations.Schema;

namespace MassTransitMQ.Domain.Entities
{
    public class Order : EntityBase
    {
        public Order(Guid id, string? number, Guid buyerId, string? address, decimal price, decimal discount, decimal deliveryPrice)
            : base(id)
        {
            Number = number;
            BuyerId = buyerId;
            Address = address;
            Price = price;
            Discount = discount;
            DeliveryPrice = deliveryPrice;
            TotalPrice = SumOrderTotalPrice();
        }

        public string? Number { get; private set; }
        public Guid BuyerId { get; private set; }
        public string? Address { get; private set; }
        public decimal Price { get; private set; }
        public decimal Discount { get; private set; }
        public decimal DeliveryPrice { get; private set; }
        public decimal TotalPrice { get; private set; }

        public virtual User? Buyer { get; private set; }

        private decimal SumOrderTotalPrice()
        {
            return (this.Price + this.DeliveryPrice) - this.Discount;
        }
    }
}