using MassTransitMQ.Domain.DTOs.Response;
using MassTransitMQ.Domain.DTOs;
using MediatR;
using MassTransitMQ.Infra.Data.Repositories.Interfaces;
using MassTransitMQ.Domain.DTOs.Queries.User;

namespace MassTransitMQ.Application.Queries.User.FindById;

public class FindByIdHandler : IRequestHandler<FindById, State<UserResponse>>
{
    private readonly IUserRepository _userRepository;

    public FindByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<State<UserResponse>> Handle(FindById request, CancellationToken cancellationToken)
    {
        var findUserState = new State<UserResponse>(200);

        var data = await _userRepository.FindByIdAsync(request.Id, cancellationToken);

        if (data == null)
        {
            findUserState.Status = 404;
            findUserState.Message = "Nenhum registro encontrado!";
            return findUserState;
        }

        findUserState.Data = new UserResponse(data.FirstName, data.LastName, data.Email, data.IsActive, data.CreatedOn);

        return findUserState;
    }
}