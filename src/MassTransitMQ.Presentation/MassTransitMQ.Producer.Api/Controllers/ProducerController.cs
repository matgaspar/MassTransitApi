using MassTransit;
using MassTransitMQ.Domain.Messaging.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace MassTransitMQ.Producer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProducerController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ProducerController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Post(OrderMessagingModel orderMessaging){
            try
            {
                var cancelToken = new CancellationToken();
                
                await _publishEndpoint.Publish<OrderMessagingModel>(orderMessaging, cancelToken);

                return Ok(new {
                    success= true,
                    status = 200,
                    Messaging = orderMessaging
                });
            }
            catch (Exception ex)
            {
                return Problem($"[ERROR] {ex.Message}");
            }
        }        
    }
}