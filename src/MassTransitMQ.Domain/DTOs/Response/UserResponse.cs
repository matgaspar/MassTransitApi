namespace MassTransitMQ.Domain.DTOs.Response;

public class UserResponse
{
    public UserResponse(string? firstName, string? lastName, string? email, bool isActive, DateTime createdOn)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        IsActive = isActive;
        CreatedOn = createdOn;
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedOn { get; set; }

}