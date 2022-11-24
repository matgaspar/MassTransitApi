using MassTransitMQ.Domain.Interfaces.Services;
using MediatR;

namespace MassTransitMQ.Application.Commands.Order.DeleteCommand;

public class DeleteOrderCommandHandler : AsyncRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderService _orderService;

    public DeleteOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    protected override async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        await _orderService.DeleteAsync(request.Id, cancellationToken);
    }
}