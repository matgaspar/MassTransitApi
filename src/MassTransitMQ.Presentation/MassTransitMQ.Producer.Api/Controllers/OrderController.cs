using MassTransitMQ.Application.Commands.Order.CreateCommand;
using MassTransitMQ.Application.Commands.Order.DeleteCommand;
using MassTransitMQ.Application.Queries.Order.FindById;
using MassTransitMQ.Application.Queries.Order.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Net.Mime;
using MassTransitMQ.Domain.DTOs.IO.Input;

namespace MassTransitMQ.Producer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(""), EnableRateLimiting("fixed")]
    public async Task<IActionResult> Get(
        [FromQuery] GetAllOrders getAll
    )
    {
        CancellationToken cancellationToken = new();

        var result = await _mediator.Send(getAll, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        CancellationToken cancellationToken = new();
            
        var state = await _mediator.Send(new FindOrderById(id), cancellationToken);

        return Ok(state);
    }

    [HttpPost, EnableRateLimiting("fixed")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Post(CreateOrderCommand createOrderCommand)
    {
        try
        {
            var cancellationToken = new CancellationToken();

            await _mediator.Send(createOrderCommand, cancellationToken);

            return Accepted();
        }
        catch (Exception ex)
        {
            return Problem($"[ERROR] {ex.Message}");
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json)]
    public Task<IActionResult> Put(OrderInput createOrderCommand)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            CancellationToken cancellationToken = new();

            await _mediator.Send(new DeleteOrderCommand(id), cancellationToken);

            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem($"[ERROR] {ex.Message}");
        }
    }
}