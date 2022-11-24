using MassTransitMQ.Domain.DTOs.IO.Input;
using MassTransitMQ.Domain.DTOs.IO.Output;

namespace MassTransitMQ.Domain.Interfaces.Services;

public interface IUserService : IService<UserInput, UserOutput, Guid>
{
}