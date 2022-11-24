using MassTransitMQ.Domain.DTOs.IO.Output;
using MassTransitMQ.Domain.DTOs.State;
using MassTransitMQ.Domain.Interfaces.Services;
using MediatR;

namespace MassTransitMQ.Application.Queries.Order.FindById;

public record FindOrderByIdHandler : IRequestHandler<FindOrderById, State<OrderOutput>>
{
    private readonly IOrderService _orderService;

    public FindOrderByIdHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<State<OrderOutput>> Handle(FindOrderById request, CancellationToken cancellationToken)
    {
        var findUserState = new State<OrderOutput>(200);

        var data = await _orderService.FindByIdAsync(request.Id, cancellationToken);

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