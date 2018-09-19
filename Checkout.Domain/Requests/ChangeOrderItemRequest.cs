namespace Checkout.Domain.Requests
{
    public class ChangeOrderItemRequest
    {
        public string OrderItemId { get; set; }
        public int Quantity { get; set; }
    }
}