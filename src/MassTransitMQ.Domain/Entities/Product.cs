namespace MassTransitMQ.Domain.Entities
{
    public class Product : EntityBase
    {
        public Product(Guid id, string sKU, string name, string description, bool isActive)
            : base(id)
        {
            SKU = sKU;
            Name = name;
            Description = description;
            IsActive = isActive;
        }

        public string SKU { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
    }
}