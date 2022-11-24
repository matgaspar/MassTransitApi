using MassTransitMQ.Domain.DTOs.IO.Input;
using MediatR;

namespace MassTransitMQ.Application.Commands.Order.CreateCommand;

public record CreateOrderCommand(string? Number, Guid BuyerId, string? Address, decimal Price, decimal Discount, decimal DeliveryPrice) 
    : OrderInput(Number, BuyerId, Address, Price, Discount, DeliveryPrice), IRequest;