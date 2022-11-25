using MassTransitMQ.Application.Commands.User.CreateUserCommand;
using MassTransitMQ.Application.Queries.User.FindById;
using MassTransitMQ.Application.Queries.User.GetAll;
using MassTransitMQ.Domain.DTOs.IO.Input;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitMQ.Producer.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] GetAllUsers getAllUsers
    )
    {
        CancellationToken cancellationToken = new();

        var users = await _mediator.Send(getAllUsers, cancellationToken);

        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        CancellationToken cancellationToken = new();
            
        var state = await _mediator.Send(new FindUserById(id), cancellationToken);

        return Ok(state);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateUserCommand createUserCommand)
    {
        CancellationToken cancellationToken = new();

        var id = await _mediator.Send(createUserCommand, cancellationToken);


        return Created($"?id={id}", id);
    }

    [HttpPut("{id:guid}")]
    public Task<IActionResult> Put(Guid id, UserInput userUpdate)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:guid}")]
    public Task<IActionResult> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}