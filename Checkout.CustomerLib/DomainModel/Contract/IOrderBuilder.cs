using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    public interface IOrderBuilder
    {
        IOrderBuilder WithItem(string name, string description, int quantity);
        Task<string> Build();
    }
}