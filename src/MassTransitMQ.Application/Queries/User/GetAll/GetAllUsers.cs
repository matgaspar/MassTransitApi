using MassTransitMQ.Domain.DTOs.IO.Output;
using MassTransitMQ.Domain.DTOs.Request;
using MassTransitMQ.Domain.DTOs.State;
using MediatR;

namespace MassTransitMQ.Application.Queries.User.GetAll;

public record GetAllUsers(int Page = 1, int PageSize = 10) 
    : PaginationRequest(Page, PageSize), IRequest<State<IEnumerable<UserOutput>>>;