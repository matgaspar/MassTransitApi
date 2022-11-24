namespace MassTransitMQ.Domain.DTOs.IO.Output;

public record UserOutput(
    Guid Id,
    string? FirstName,
    string? LastName,
    string? Email,
    bool IsActive,
    DateTime CreatedOn,
    DateTime UpdatedOn
);