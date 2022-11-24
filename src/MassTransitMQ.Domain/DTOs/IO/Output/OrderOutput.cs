namespace MassTransitMQ.Domain.DTOs.IO.Output;

public record OrderOutput(
    Guid Id,
    string? Number, 
    Guid BuyerId, 
    string? Address, 
    decimal Price,
    decimal Discount,
    decimal DeliveryPrice,
    decimal TotalPrice,
    DateTime CreatedOn,
    DateTime UpdatedOn
);