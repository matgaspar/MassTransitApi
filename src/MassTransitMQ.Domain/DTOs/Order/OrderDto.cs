namespace MassTransitMQ.Domain.DTOs.Order;

public class OrderDto
{
    public OrderDto(string? number, Guid buyerId, string? address, decimal price, decimal discount, decimal deliveryPrice)
    {
        Number = number;
        BuyerId = buyerId;
        Address = address;
        Price = price;
        Discount = discount;
        DeliveryPrice = deliveryPrice;
    }

    public string? Number { get; private set; }
    public Guid BuyerId { get; private set; }
    public string? Address { get; private set; }
    public decimal Price { get; private set; }
    public decimal Discount { get; private set; }
    public decimal DeliveryPrice { get; private set; }
}