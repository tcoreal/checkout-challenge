using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    internal class OrdersApiProxy : IOrdersApiProxy
    {
        private readonly string _apiUrl;
        private readonly ILoggerWriter _loggerWriter;
        private readonly IHttpRequester _httpRequester;
        public OrdersApiProxy(string apiUrl, ILoggerWriter loggerWriter, IJsonSerializer jsonSerializer)
        {
            _loggerWriter = loggerWriter ?? new EmptyLoggerWriter();
            _httpRequester = new HttpRequester(jsonSerializer, _loggerWriter);
            _apiUrl = apiUrl;
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrders()
        {
            _loggerWriter.Debug("GetAllOrders triggered");
            var result = await _httpRequester.ProcessGetRequest<List<OrderResponse>>($"{_apiUrl}/");
            _loggerWriter.Debug("GetAllOrders succeeded");
            return result;
        }

        public async Task<OrderResponse> GetOrderById(string orderId)
        {
            _loggerWriter.Debug($"GetOrderById with orderId:{orderId} triggered");
            var result = await _httpRequester.ProcessGetRequest<OrderResponse>($"{_apiUrl}/{orderId}/");
            _loggerWriter.Debug($"GetOrderById with orderId:{orderId} succeeded");
            return result;
        }

        public async Task<string> CreateOrder()
        {
            _loggerWriter.Debug("CreateOrder triggered");
            var result = await _httpRequester.ProcessPostRequest<string, string>($"{_apiUrl}/create/",
                deserializeFunc: (response, serializer) => response);
            _loggerWriter.Debug($"CreateOrder was succeeded with orderId:{result} ");
            return result;
        }

        public async Task<string> AddItemToOrder(string orderId, CreateOrderItemRequest request)
        {
            _loggerWriter.Debug($"AddItemToOrder triggered for orderId:{orderId}");
            var result = await _httpRequester.ProcessPostRequest($"{_apiUrl}/addToOrder/{orderId}/", request,
                (response, serializer) => response);
            _loggerWriter.Debug($"AddItemToOrder was succeeded with orderId:{result} and orderItemId:{result} ");
            return result;
        }

        public async Task ChangeOrderItemQuantity(string orderId, ChangeOrderItemRequest request)
        {
            _loggerWriter.Debug($"ChangeOrderItemQuantity triggered for orderId:{orderId}");
            await _httpRequester.ProcessPostRequestWithoutResponse($"{_apiUrl}/changeOrderItem/{orderId}/", request);
            _loggerWriter.Debug($"ChangeOrderItemQuantity was succeeded with orderId:");
        }

        public async Task RemoveOrderItemQuantity(string orderId, RemoveOrderItemRequest request)
        {
            _loggerWriter.Debug($"RemoveOrderItemQuantity triggered for orderId:{orderId}");
            await _httpRequester.ProcessPostRequestWithoutResponse($"{_apiUrl}/removeOrderItem/{orderId}/", request);
            _loggerWriter.Debug($"RemoveOrderItemQuantity was succeeded with orderId:{orderId}");
        }

        public async Task PurgeOrder(string orderId)
        {
            _loggerWriter.Debug($"PurgeOrder triggered for orderId:{orderId}");
            await _httpRequester.ProcessPostRequestWithoutResponse<string>($"{_apiUrl}/purge/{orderId}/");
            _loggerWriter.Debug($"PurgeOrder was succeeded with orderId:{orderId}");
        }
    }
}