using MassTransitMQ.Domain.DTOs.IO.Output;
using MassTransitMQ.Domain.DTOs.State;
using MassTransitMQ.Domain.Interfaces.Services;
using MediatR;

namespace MassTransitMQ.Application.Queries.Order.GetAll;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrders, State<IEnumerable<OrderOutput>>>
{
    private readonly IOrderService _orderService;

    public GetAllOrdersHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<State<IEnumerable<OrderOutput>>> Handle(GetAllOrders request, CancellationToken cancellationToken)
    {
        var getAll = new State<IEnumerable<OrderOutput>>();

        var data = await _orderService.GetAllAsync(request.Page, request.PageSize, cancellationToken);

        if (data == null || !data.Any())
        {
            getAll.Status = 404;
            getAll.Message = "Nenhum registro encontrado!";
            return getAll;
        }

        getAll.Data = data;

        return getAll;
    }
}