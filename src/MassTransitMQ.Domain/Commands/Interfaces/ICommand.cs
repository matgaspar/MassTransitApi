namespace MassTransitMQ.Domain.Commands.Interfaces
{
    public interface ICommand
    {
        Task Action();
    }
}