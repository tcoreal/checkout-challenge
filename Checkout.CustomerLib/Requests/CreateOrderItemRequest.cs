namespace Checkout.CustomerLib
{
    internal class CreateOrderItemRequest
    {
        public CreateOrderItemRequest(int quantity, string name, string description)
        {
            Quantity = quantity;
            Name = name;
            Description = description;
        }

        public int Quantity { get; }
        public string Name { get; }
        public string Description { get; }
    }
}