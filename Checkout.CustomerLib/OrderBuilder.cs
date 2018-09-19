using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    internal class OrderBuilder : IOrderBuilder
    {
        private readonly IOrdersApiProxy _apiProxy;
        private readonly List<(string name, string description, int quantity)> _items;

        public OrderBuilder(IOrdersApiProxy apiProxy)
        {
            _apiProxy = apiProxy;
            _items = new List<(string name, string description, int quantity)>();
        }


        public IOrderBuilder WithItem(string name, string description, int quantity)
        {
            _items.Add((name,description,quantity));
            return this;
        }

        public async Task Build()
        {
            var orderId = await _apiProxy.CreateOrder();
            foreach (var item in _items)
            {
                await _apiProxy.AddItemToOrder(orderId,
                    new CreateOrderItemRequest(item.quantity, item.name, item.description));
            }
        }
    }
}