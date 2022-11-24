using MassTransitMQ.Domain.DTOs.IO.Output;
using MassTransitMQ.Domain.DTOs.Request;
using MassTransitMQ.Domain.DTOs.State;
using MediatR;

namespace MassTransitMQ.Application.Queries.Order.GetAll;

public record GetAllOrders(int Page, int PageSize) 
    : PaginationRequest(Page, PageSize), IRequest<State<IEnumerable<OrderOutput>>>;