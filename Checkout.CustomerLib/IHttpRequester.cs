using System.Threading.Tasks;

namespace Checkout.CustomerLib
{
    internal interface IHttpRequester
    {
        Task<TResponse> ProcessGetRequest<TResponse>(string requestUrl);
        Task<TResponse> ProcessPostRequest<TRequest, TResponse>(string requestUrl, TRequest request = default(TRequest));
        Task ProcessPostRequestWithoutResponse<TRequest>(string requestUrl, TRequest request= default(TRequest));
    }
}