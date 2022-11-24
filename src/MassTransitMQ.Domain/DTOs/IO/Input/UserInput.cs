namespace MassTransitMQ.Domain.DTOs.IO.Input;

public record UserInput(
    string? FirstName,
    string? LastName,
    string? Email,
    string? Password
);