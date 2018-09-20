namespace Checkout.CustomerLib
{
    public interface IJsonSerializer
    {
        string Serialize<T>(T input);
        T Deserialize<T>(string input);
    }
}