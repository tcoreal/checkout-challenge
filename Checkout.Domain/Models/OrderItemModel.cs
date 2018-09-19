namespace Checkout.Domain.Models
{
    public class OrderItemModel
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}