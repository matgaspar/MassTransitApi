using MassTransitMQ.Domain.Interfaces.Data.Repositories;
using MediatR;

namespace MassTransitMQ.Application.Commands.User.CreateUserCommand;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Domain.Entities.User(request.FirstName, request.LastName, request.Email, request.Password, true, DateTime.Now);

        var id = await _userRepository.AddAsync(user, cancellationToken);

        return id;
    }
}