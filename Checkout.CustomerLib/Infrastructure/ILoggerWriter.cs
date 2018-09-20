using System;

namespace Checkout.CustomerLib
{
    public interface ILoggerWriter
    {
        void Debug(string message);
        void Error(string message, Exception exception);
    }
}