using MassTransitMQ.Domain.DTOs;
using MassTransitMQ.Domain.DTOs.Response;
using MediatR;

namespace MassTransitMQ.Application.Queries.User.FindById;

public class FindById : IRequest<State<UserResponse>>
{
    public Guid Id { get; set; }
}