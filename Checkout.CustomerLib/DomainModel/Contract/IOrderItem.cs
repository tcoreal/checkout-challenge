using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    public interface IOrderItem
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
        int Quantity { get; }
        bool IsDeleted { get; }
        Task ChangeQuantity(int quantity);
        Task RemoveItem();
    }
}