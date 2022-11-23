using MassTransitMQ.Domain.DTOs.Interfaces;

namespace MassTransitMQ.Domain.DTOs;

public class State<TData> : IState
{
    public State()
    {
        this.Data = default;
    }

    public State(int status)
        : this()
    {
        Status = status;
    }

    public State(int status, string? message)
        : this(status)
    {
        Message = message;
    }

    public State(int status, string? message, TData data)
        : this(status)
    {
        Message = message;
        Data = data;
    }

    public int Status { get; set; }
    public string? Message { get; set; }
    public TData? Data { get; set; }

}