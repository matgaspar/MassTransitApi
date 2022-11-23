using MassTransitMQ.Application.Commands.User.CreateUserCommand;
using MassTransitMQ.Application.Queries.User.FindById;
using MassTransitMQ.Application.Queries.User.GetAllUsersQuery;
using MassTransitMQ.Domain.Entities;
using MassTransitMQ.Infra.Data.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMQ.Producer.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public UserController(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get(
            [FromQuery] GetAllUsersQuery getAllUsersQuery
        )
        {
            CancellationToken cancellationToken = new();

            var users = await _mediator.Send(getAllUsersQuery, cancellationToken);

            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            CancellationToken cancellationToken = new();

            FindById findById = new() { Id = id };

            var state = await _mediator.Send(findById, cancellationToken);

            return Ok(state);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(CreateUserCommand createUserCommand)
        {
            CancellationToken cancellationToken = new();

            var id = await _mediator.Send(createUserCommand, cancellationToken);


            return Created($"?id={id}", id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, User userUpdate)
        {
            CancellationToken cancellationToken = new();
            userUpdate.SetId(id);

            await _userRepository.UpdateAsync(userUpdate, cancellationToken);
            
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Put(Guid id)
        {
            CancellationToken cancellationToken = new();
            await _userRepository.DeleteAsync(id, cancellationToken);
            
            return NoContent();
        }
    }
}