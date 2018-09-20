using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    internal class Order : IOrder
    {
        private readonly IOrdersApiProxy _apiProxy;
        private readonly List<OrderItem> _items;

        public Order(IOrdersApiProxy apiProxy, string id, List<OrderItem> items)
        {
            _apiProxy = apiProxy;
            Id = id;
            _items = items;
        }

        public string Id { get; }
        public async Task Purge()
        {
            await _apiProxy.PurgeOrder(Id);
            _items.Clear();
        }

        public async Task AddOrderItem(string name, string description, int quantity)
        {
            var orderItemId =
                await _apiProxy.AddItemToOrder(Id, new CreateOrderItemRequest(quantity, name, description));
            _items.Add(new OrderItem(_apiProxy, Id, orderItemId, name, description, quantity));
        }

        public IEnumerable<IOrderItem> Items => _items;
    }
}