using MassTransitMQ.Application.Queries.Order.FindById;
using MassTransitMQ.Application.Queries.Order.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using MassTransitMQ.Application.Commands.Order.CreateCommand;
using MassTransitMQ.Application.Commands.Order.DeleteCommand;

namespace MassTransitMQ.Producer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get(
            [FromQuery] GetAllOrders getAll
        )
        {
            CancellationToken cancellationToken = new();

            var users = await _mediator.Send(getAll, cancellationToken);

            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            CancellationToken cancellationToken = new();
            
            var state = await _mediator.Send(new FindOrderById(id), cancellationToken);

            return Ok(state);
        }

        [HttpPost]
        [Route("")]
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

        [HttpPut]
        [Route("")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Put(CreateOrderCommand createOrderCommand)
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

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                CancellationToken cancellationToken = new();

                DeleteOrderCommand deleteOrderCommand = new(id);

                await _mediator.Send(deleteOrderCommand, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"[ERROR] {ex.Message}");
            }
        }
    }
}

/*
    {
      "number": "123321",
      "buyerId": "08dacd0c-fc1a-4de4-8d69-f6d3c0fbb8b8",
      "address": "Av. Beira Rio, 515, Apto 502 - Piraí/RJ",
      "price": 154,
      "discount": 59,
      "deliveryPrice": 10
    }
 * 
 */