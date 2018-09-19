namespace Checkout.DataAccess.InMemory.Entities
{
    public class OrderItemEntity
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}