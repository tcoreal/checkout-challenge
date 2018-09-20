using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Checkout.CustomerWebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Checkout.IntegrationTests
{
    public class ApiIntegrationTest
    {
        private TestServer _testServer;
        private readonly HttpClient _testClient;

        public ApiIntegrationTest()
        {
            _testServer = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _testClient = _testServer.CreateClient();
        }

        [Fact]
        public async Task TestCreateOrder()
        {
            //arrange
            var content = new StringContent("", Encoding.UTF8, MediaTypeNames.Application.Json);
            var apiOrdersCreate = "/api/orders/create";

            //act
            var response = await _testClient.PostAsync(apiOrdersCreate, content);
            
            //assert
            response.EnsureSuccessStatusCode();
            var orderId = await response.Content.ReadAsStringAsync();

            var wasParsed = Guid.TryParse(orderId, out var parsedId);
            Assert.True(wasParsed);

        }
    }
}
