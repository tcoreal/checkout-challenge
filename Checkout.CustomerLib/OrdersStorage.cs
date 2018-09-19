using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    public class OrdersStorage
    {
        private readonly IOrdersProvider _ordersProvider;
        public OrdersStorage(string apiUrl, ILoggerWriter loggerWriter = null, IJsonSerializer jsonSerializer = null)
        {
            _ordersProvider = new OrdersProvider(apiUrl, loggerWriter, jsonSerializer);
        }

        public Task<IOrder> GetOrderById(string orderId) => _ordersProvider.GetOrder(orderId);

        public Task<IEnumerable<IOrder>> GetAllOrders() => _ordersProvider.GetAllOrders();
    }
}