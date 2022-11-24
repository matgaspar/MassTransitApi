using MediatR;

namespace MassTransitMQ.Application.Commands.Order.DeleteCommand;

public record DeleteOrderCommand(Guid Id) : IRequest;