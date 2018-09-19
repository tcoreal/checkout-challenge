using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    internal class OrderItem : IOrderItem
    {
        private readonly IOrdersApiProxy _apiProxy;
        private readonly string _orderId;

        public OrderItem(IOrdersApiProxy apiProxy, string orderId, string id, string name, string description, int quantity)
        {
            _apiProxy = apiProxy;
            _orderId = orderId;
            Id = id;
            Name = name;
            Description = description;
            Quantity = quantity;
        }

        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int Quantity { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public async Task ChangeQuantity(int quantity)
        {
            await _apiProxy.ChangeOrderItemQuantity(_orderId,
                new ChangeOrderItemRequest(Id, quantity));
            Quantity = quantity;
        }

        public async Task RemoveItem()
        {
            await _apiProxy.RemoveOrderItemQuantity(_orderId, new RemoveOrderItemRequest(Id));
            IsDeleted = true;
        }
    }
}