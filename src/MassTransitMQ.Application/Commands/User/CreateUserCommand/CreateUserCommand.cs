using MassTransitMQ.Domain.DTOs.IO.Input;
using MassTransitMQ.Domain.DTOs.User;
using MediatR;

namespace MassTransitMQ.Application.Commands.User.CreateUserCommand;

public record CreateUserCommand(string? FirstName, string? LastName, string? Email, string? Password) 
    : UserInput(FirstName, LastName, Email, Password), IRequest<Guid>;