namespace Checkout.Domain.Requests
{
    public class CreateOrderItemRequest
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}