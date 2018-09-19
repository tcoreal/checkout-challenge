using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.Domain.Models;
using Checkout.Domain.Requests;

namespace Checkout.BusinessCommon
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<OrderModel>> GetAllOrdersForUser(ulong userId);
        Task<OrderModel> GetOrderById(ulong userId, string orderId);
        Task<string> CreateOrderForUser(ulong userId);
        Task AddItemToOrder(ulong userId, string orderId, CreateOrderItemRequest request);
        Task ChangeOrderItemQuantity(ulong userId, string orderId, string orderItemId, int quantity);
        Task RemoveItemFromOrder(ulong userId, string orderId, string orderItemId);
        Task PurgeOrder(ulong userId, string orderId);
    }
}