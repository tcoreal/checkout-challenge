using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    public interface IOrder
    {
        string Id { get; }
        Task Purge();
        IEnumerable<IOrderItem> Items { get; }
    }
}