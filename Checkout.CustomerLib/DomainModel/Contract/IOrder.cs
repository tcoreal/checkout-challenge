using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    public interface IOrder
    {
        string Id { get; }
        Task Purge();
        Task AddOrderItem(string name, string description, int quantity);
        IEnumerable<IOrderItem> Items { get; }
    }
}