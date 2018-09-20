using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    internal class OrdersProvider : IOrdersProvider
    {
        private readonly IOrdersApiProxy _apiProxy;
        public OrdersProvider(string apiUrl, ILoggerWriter loggerWriter, IJsonSerializer jsonSerializer)
        {
            _apiProxy = new OrdersApiProxy(apiUrl, loggerWriter, jsonSerializer);
        }

        public async Task<IOrder> GetOrder(string orderId)
        {
            var order = await _apiProxy.GetOrderById(orderId);
            return MapOrderResponseToOrder(order);
        }

        public async Task<IEnumerable<IOrder>> GetAllOrders()
        {
            var orders = await _apiProxy.GetAllOrders();
            return orders.Select(MapOrderResponseToOrder).ToArray();
        }

        private Order MapOrderResponseToOrder(OrderResponse order)
        {
            var orderItems = order.OrderItems
                .Select(x => new OrderItem(_apiProxy, order.Id, x.Id, x.Name, x.Description, x.Quantity))
                .ToList();
            return new Order(_apiProxy, order.Id, orderItems);
        }
    }
}