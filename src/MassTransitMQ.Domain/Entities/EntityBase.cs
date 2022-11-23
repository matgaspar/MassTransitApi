namespace MassTransitMQ.Domain.Entities
{
    public class EntityBase
    {
        public EntityBase() { }

        public EntityBase(Guid id) 
            : this()
        {
            Id = id;
        }

        public EntityBase(DateTime createdOn)
            : this()
        {
            CreatedOn = createdOn;
        }

        public EntityBase(Guid id, DateTime createdOn)
            : this(id)
        {
            CreatedOn = createdOn;
        }

        public EntityBase(Guid id, DateTime createdOn, DateTime updatedOn) 
            : this(id, createdOn)
        {
            UpdatedOn = updatedOn;
        }

        public Guid Id { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public DateTime UpdatedOn { get; protected set; }
    }
}