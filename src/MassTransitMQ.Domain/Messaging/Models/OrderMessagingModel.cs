namespace MassTransitMQ.Domain.Messaging.Models
{
    public class OrderMessagingModel
    {
        #region Constructor
        public OrderMessagingModel(Guid? id, string? number, string? buyerName, string? address)
        {
            Id = id;
            Number = number;
            BuyerName = buyerName;
            Address = address;
            CreatedOn = DateTime.Now;
        }
        #endregion

        #region Props
        public Guid? Id { get; private set; }
        public string? Number { get; private set; }
        public string? BuyerName { get; private set; }
        public string? Address { get; private set; }
        public DateTime CreatedOn { get; private set; }
        #endregion

        #region Methods
        public override string ToString() => $"[{this.Number}] {this.BuyerName} - {this.Address} | {this.CreatedOn}";
        #endregion
    }
}