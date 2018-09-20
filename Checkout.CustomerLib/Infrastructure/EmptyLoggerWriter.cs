using System;

namespace Checkout.CustomerLib
{
    internal class EmptyLoggerWriter : ILoggerWriter
    {
        public void Debug(string message)
        {
            
        }

        public void Error(string message, Exception exception)
        {
        }
    }
}