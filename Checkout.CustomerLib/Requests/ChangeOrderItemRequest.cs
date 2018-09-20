namespace Checkout.CustomerLib
{
    internal class ChangeOrderItemRequest
    {
        public ChangeOrderItemRequest(string orderItemId, int quantity)
        {
            OrderItemId = orderItemId;
            Quantity = quantity;
        }

        public string OrderItemId { get; }
        public int Quantity { get; }
    }
}