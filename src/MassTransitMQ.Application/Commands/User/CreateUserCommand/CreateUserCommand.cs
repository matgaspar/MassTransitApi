using MassTransitMQ.Domain.DTOs.Commands.User;
using MediatR;

namespace MassTransitMQ.Application.Commands.User.CreateUserCommand;

public class CreateUserCommand : CreateUser, IRequest<Guid>
{
    public CreateUserCommand(string? firstName, string? lastName, string? email, string? password)
        : base(firstName, lastName, email, password) { }
}