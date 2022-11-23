namespace MassTransitMQ.Domain.DTOs.Commands.User;

public class CreateUser
{
    public CreateUser(string? firstName, string? lastName, string? email, string? password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? Password { get; init; }

}