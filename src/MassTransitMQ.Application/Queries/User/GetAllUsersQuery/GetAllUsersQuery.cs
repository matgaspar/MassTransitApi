using MassTransitMQ.Domain.DTOs.Queries.User;
using MassTransitMQ.Domain.DTOs.Request;
using MediatR;

namespace MassTransitMQ.Application.Queries.User.GetAllUsersQuery;

public class GetAllUsersQuery : PaginationRequest, IRequest<GetAllUsersResponse> { }