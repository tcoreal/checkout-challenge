using System;
using System.Collections.Generic;

namespace Checkout.Domain
{
    public class OrderModel
    {
        public ulong UserId { get; set; }

        public IEnumerable<OrderItemModel> OrderItems { get; set; }
    }
}
