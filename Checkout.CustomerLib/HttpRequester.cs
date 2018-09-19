using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    internal class HttpRequester : IHttpRequester
    {
        private readonly ILoggerWriter _loggerWriter;
        private readonly HttpClient _httpClient;
        private readonly IJsonSerializer _jsonSerializer;

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

        public async Task<TResponse> ProcessPostRequest<TRequest, TResponse>(string requestUrl, TRequest request = default(TRequest))
        {
            string requestBody = null;
            try
            {
                StringContent stringContent = new StringContent("");
                if (!Equals(request, default(TRequest)))
                {
                    requestBody = _jsonSerializer.Serialize(request);
                    stringContent = new StringContent(requestBody);
                }

                var response = await _httpClient.PostAsync(requestUrl, stringContent);
                var stringResponse = await response.Content.ReadAsStringAsync();
                return _jsonSerializer.Deserialize<TResponse>(stringResponse);
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
                await _httpClient.PostAsync(requestUrl, new StringContent(requestBody));
            }
            catch (Exception ex)
            {
                _loggerWriter.Error($"Exception during processing post request:{requestUrl}, with body:{requestBody}", ex);
                throw;
            }
        }
    }
}