using MassTransitMQ.Domain.DTOs.Response;

namespace MassTransitMQ.Domain.DTOs.Queries.User;

public class GetAllUsersResponse : State<IList<UserResponse>>
{
}