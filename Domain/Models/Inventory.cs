namespace Market.Domain.Entities
{
    public class Inventory : BaseEntity
    {
        public int QuantityOnHand { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
