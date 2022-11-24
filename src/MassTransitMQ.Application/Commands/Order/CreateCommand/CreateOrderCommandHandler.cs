using MassTransit;
using MediatR;

namespace MassTransitMQ.Application.Commands.Order.CreateCommand;

public class CreateOrderCommandHandler : AsyncRequestHandler<CreateCommand.CreateOrderCommand>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateOrderCommandHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    protected override async Task Handle(CreateCommand.CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(request, cancellationToken);
    }
}