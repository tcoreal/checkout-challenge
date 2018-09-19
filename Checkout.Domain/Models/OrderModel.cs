using System.Collections.Generic;

namespace Checkout.Domain.Models
{
    public class OrderModel
    {
        public string Id { get; set; }
        public ulong UserId { get; set; }
        public IEnumerable<OrderItemModel> OrderItems { get; set; }
    }
}
