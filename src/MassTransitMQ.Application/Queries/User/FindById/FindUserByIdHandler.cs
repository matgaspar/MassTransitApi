using MassTransitMQ.Domain.DTOs.IO.Output;
using MassTransitMQ.Domain.DTOs.State;
using MassTransitMQ.Domain.Interfaces.Services;
using MediatR;

namespace MassTransitMQ.Application.Queries.User.FindById;

public class FindUserByIdHandler : IRequestHandler<FindUserById, State<UserOutput>>
{
    private readonly IUserService _userService;

    public FindUserByIdHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<State<UserOutput>> Handle(FindUserById request, CancellationToken cancellationToken)
    {
        var findUserState = new State<UserOutput>(200);

        var data = await _userService.FindByIdAsync(request.Id, cancellationToken);

        if (data == null)
        {
            findUserState.Status = 404;
            findUserState.Message = "Nenhum registro encontrado!";
            return findUserState;
        }

        findUserState.Data = data;

        return findUserState;
    }
}