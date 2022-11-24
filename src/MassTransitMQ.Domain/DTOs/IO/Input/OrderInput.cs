namespace MassTransitMQ.Domain.DTOs.IO.Input;

public record OrderInput(
    string? Number,
    Guid BuyerId,
    string? Address,
    decimal Price,
    decimal Discount,
    decimal DeliveryPrice
);