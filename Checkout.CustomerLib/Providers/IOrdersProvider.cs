using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    internal interface IOrdersProvider
    {
        Task<IOrder> GetOrder(string orderId);
        Task<IEnumerable<IOrder>> GetAllOrders();
    }
}