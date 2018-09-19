using System;

namespace Checkout.Domain.Exceptions
{
    public class OrderItemNotFoundException : Exception
    {
        public OrderItemNotFoundException( string orderItemId, string orderId)
            : base($"Order item with Id: {orderItemId} for order: {orderId} does not exist")
        {
        }
    }
}