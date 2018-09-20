using Newtonsoft.Json;

namespace Checkout.CustomerLib
{
    internal class NewtonsoftSerializer : IJsonSerializer
    {
        public string Serialize<T>(T input) => JsonConvert.SerializeObject(input);

        public T Deserialize<T>(string input) =>  JsonConvert.DeserializeObject<T>(input);
    }
}