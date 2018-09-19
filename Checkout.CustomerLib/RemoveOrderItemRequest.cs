namespace Checkout.CustomerLib
{
    internal class RemoveOrderItemRequest
    {
        public RemoveOrderItemRequest(string orderItemId)
        {
            OrderItemId = orderItemId;
        }

        public string OrderItemId { get; }
    }
}