using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    internal interface IOrdersApiProxy
    {
        Task<IEnumerable<OrderResponse>> GetAllOrders();
        Task<OrderResponse> GetOrderById(string orderId);
        Task<string> CreateOrder();
        Task<string> AddItemToOrder(string orderId, CreateOrderItemRequest request);
        Task ChangeOrderItemQuantity(string orderId, ChangeOrderItemRequest request);
        Task RemoveOrderItemQuantity(string orderId, RemoveOrderItemRequest request);
        Task PurgeOrder(string orderId);
    }
}