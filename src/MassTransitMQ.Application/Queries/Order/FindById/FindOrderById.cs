using MassTransitMQ.Domain.DTOs.IO.Output;
using MassTransitMQ.Domain.DTOs.State;
using MediatR;

namespace MassTransitMQ.Application.Queries.Order.FindById;

public record FindOrderById(Guid Id) : IRequest<State<OrderOutput>>;