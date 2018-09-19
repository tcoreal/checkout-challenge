namespace Checkout.CustomerLib
{
    internal class OrderItemResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}