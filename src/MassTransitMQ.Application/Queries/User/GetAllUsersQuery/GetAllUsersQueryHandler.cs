using MassTransitMQ.Domain.DTOs.Queries.User;
using MassTransitMQ.Domain.DTOs.Response;
using MassTransitMQ.Infra.Data.Repositories.Interfaces;
using MediatR;

namespace MassTransitMQ.Application.Queries.User.GetAllUsersQuery;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersResponse>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetAllUsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var getAllUsersState = new GetAllUsersResponse();

        var data = await _userRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        if (data == null)
        {
            getAllUsersState.Status = 404;
            getAllUsersState.Message = "Nenhum registro encontrado!";
            return getAllUsersState;
        }

        getAllUsersState.Data = data?.Select(user => new UserResponse(user.FirstName, user.LastName, user.Email, user.IsActive, user.CreatedOn))?.ToList();

        return getAllUsersState;
    }
}