namespace Checkout.CustomerLib
{
    public class OrdersCreator
    {
        private readonly IOrdersApiProxy _apiProxy;
        public OrdersCreator(string apiUrl, ILoggerWriter loggerWriter = null, IJsonSerializer jsonSerializer = null)
        {
            _apiProxy = new OrdersApiProxy(apiUrl, loggerWriter, jsonSerializer);
        }

        public IOrderBuilder CreateOrder() => new OrderBuilder(_apiProxy);
    }
}
