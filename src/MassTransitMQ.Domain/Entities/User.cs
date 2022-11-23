namespace MassTransitMQ.Domain.Entities
{
    public class User : EntityBase
    {
        public User(string? firstName, string? lastName, string? email, string? password, bool isActive, DateTime createdOn)
            : base(createdOn)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            IsActive = isActive;
        }

        public User(Guid id, string? firstName, string? lastName, string? email, string? password, bool isActive, DateTime createdOn)
            : base(id, createdOn)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            IsActive = isActive;
        }
        public User(Guid id, string? firstName, string? lastName, string? email, string? password, bool isActive, DateTime createdOn, DateTime updatedOn)
            : base(id, createdOn, updatedOn)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            IsActive = isActive;
        }

        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public bool IsActive { get; private set; }

        public void SetId(Guid id)
        {
            this.Id = id;
        }
    }
}