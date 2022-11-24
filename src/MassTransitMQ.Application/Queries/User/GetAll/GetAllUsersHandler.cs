using MassTransitMQ.Domain.DTOs.IO.Output;
using MassTransitMQ.Domain.DTOs.State;
using MassTransitMQ.Domain.Interfaces.Services;
using MediatR;

namespace MassTransitMQ.Application.Queries.User.GetAll;

public class GetAllUsersHandler : IRequestHandler<GetAll.GetAllUsers, State<IEnumerable<UserOutput>>>
{
    private readonly IUserService _userService;

    public GetAllUsersHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<State<IEnumerable<UserOutput>>> Handle(GetAll.GetAllUsers request, CancellationToken cancellationToken)
    {
        var getAll = new State<IEnumerable<UserOutput>>();

        var data = await _userService.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        if (data == null)
        {
            getAll.Status = 404;
            getAll.Message = "Nenhum registro encontrado!";
            return getAll;
        }

        getAll.Data = data;

        return getAll;
    }
}