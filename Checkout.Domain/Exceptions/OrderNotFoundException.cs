using System;

namespace Checkout.Domain.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(string orderId, ulong userId)
            : base($"Order with Id: {orderId} for userId: {userId} does not exist")
        {
        }
    }
}