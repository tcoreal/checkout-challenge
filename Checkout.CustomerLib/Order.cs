using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    internal class Order : IOrder
    {
        private readonly IOrdersApiProxy _apiProxy;

        public Order(IOrdersApiProxy apiProxy, string id, IEnumerable<IOrderItem> items)
        {
            _apiProxy = apiProxy;
            Id = id;
            Items = items;
        }

        public string Id { get; }
        public Task Purge() => _apiProxy.PurgeOrder(Id);

        public IEnumerable<IOrderItem> Items { get; }
    }
}