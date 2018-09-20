using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    internal class HttpRequester : IHttpRequester
    {
        private readonly ILoggerWriter _loggerWriter;
        private readonly HttpClient _httpClient;
        private readonly IJsonSerializer _jsonSerializer;

        public delegate TResponse ResponseSerializeDelegate<TResponse>(string response, IJsonSerializer serializer);
        public HttpRequester(IJsonSerializer jsonSerializer, ILoggerWriter loggerWriter)
        {
            _loggerWriter = loggerWriter;
            _jsonSerializer = jsonSerializer ?? new NewtonsoftSerializer();
            _httpClient = new HttpClient();
        }

        public async Task<TResponse> ProcessGetRequest<TResponse>(string requestUrl)
        {
            try
            {
                var response = await _httpClient.GetStringAsync(requestUrl);
                return _jsonSerializer.Deserialize<TResponse>(response);
            }
            catch (Exception ex)
            {
                _loggerWriter.Error($"Exception during processing get request:{requestUrl}", ex);
                throw;
            }
        }

        public async Task<TResponse> ProcessPostRequest<TRequest, TResponse>(string requestUrl,
            TRequest request = default(TRequest), ResponseSerializeDelegate<TResponse> deserializeFunc = null)
        {
            string requestBody = null;
            try
            {
                requestBody = Equals(request, default(TRequest)) ? "" : _jsonSerializer.Serialize(request);
                var deserialize = deserializeFunc ?? DeserializeByDefault<TResponse>;
                var response = await _httpClient.PostAsync(requestUrl, BuildStringContent(requestBody));
                var stringResponse = await response.Content.ReadAsStringAsync();

                return deserialize(stringResponse, _jsonSerializer);
            }
            catch (Exception ex)
            {
                _loggerWriter.Error($"Exception during processing post request:{requestUrl}, with body:{requestBody}", ex);
                throw;
            }
        }

        public async Task ProcessPostRequestWithoutResponse<TRequest>(string requestUrl, TRequest request)
        {
            string requestBody = null;
            try
            {
                requestBody = _jsonSerializer.Serialize(request);
                await _httpClient.PostAsync(requestUrl, BuildStringContent(requestBody));
            }
            catch (Exception ex)
            {
                _loggerWriter.Error($"Exception during processing post request:{requestUrl}, with body:{requestBody}", ex);
                throw;
            }
        }

        private TResponse DeserializeByDefault<TResponse>(string response, IJsonSerializer serializer) =>
            serializer.Deserialize<TResponse>(response);

        private static StringContent BuildStringContent(string requestBody) =>
            new StringContent(requestBody, Encoding.UTF8, MediaTypeNames.Application.Json);
    }
}