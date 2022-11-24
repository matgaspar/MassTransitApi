using MassTransitMQ.Domain.DTOs.IO.Output;
using MassTransitMQ.Domain.DTOs.State;
using MediatR;

namespace MassTransitMQ.Application.Queries.User.FindById;

public record FindUserById(Guid Id) : IRequest<State<UserOutput>>;