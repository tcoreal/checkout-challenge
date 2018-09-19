using System.Collections.Generic;

namespace Checkout.CustomerLib
{
    internal class OrderResponse
    {
        public string Id { get; set; }
        public List<OrderItemResponse> Items { get; set; }
    }
}