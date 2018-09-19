using System;

namespace Checkout.CustomerLib
{
    public interface ILoggerWriter
    {
        void Debug(string message);
        void Error(string message, Exception exception);
    }

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