using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.CustomerLib.Demo
{
    class Program
    {
        private const string ApiUrl = "http://localhost:5000/api/orders";
        static void Main(string[] args)
        {
            ExecuteDemo().Wait();
        }

        private static async Task ExecuteDemo()
        {
            var orderId = await CreateOrders();

            var storage = new OrdersStorage(ApiUrl, new ConsoleLoggerWriter());

            var mainOrder = await storage.GetOrderById(orderId);
            await mainOrder.AddOrderItem("Xiaomi Mi10", "Brand new chinese phone", 4);

            var iphoneItems = new List<IOrderItem>();
            Console.WriteLine("Orders in storage");
            foreach (var order in await storage.GetAllOrders())
            {
                Console.WriteLine($"Order with Id:{order.Id}");
                foreach (var orderItem in order.Items)
                {
                    PrintOrderItem(orderItem);
                    if (orderItem.Name.Contains("Iphone"))
                    {
                        iphoneItems.Add(orderItem);
                        continue;
                    }

                    if (orderItem.Name.Contains("Samsung"))
                    {
                        await orderItem.RemoveItem();
                    }
                }
            }

            foreach (var item in iphoneItems)
            {
                await item.ChangeQuantity(999);
            }

            Console.WriteLine("Orders after change");
            await PrintStorage(storage);
            Console.WriteLine("Purging all orders");
            foreach (var order in await storage.GetAllOrders())
            {
                await order.Purge();
            }

            await PrintStorage(storage);
        }

        private static async Task PrintStorage(OrdersStorage storage)
        {
            foreach (var order in await storage.GetAllOrders())
            {
                Console.WriteLine($"Order with Id:{order.Id}");
                foreach (var orderItem in order.Items)
                {
                    PrintOrderItem(orderItem);
                }
            }
        }

        private static void PrintOrderItem(IOrderItem orderItem)
        {
            Console.WriteLine(
                $"Order item - Id:{orderItem.Id}, Name:{orderItem.Name}, " +
                $"Description:{orderItem.Description}, Quantity:{orderItem.Quantity}");
        }

        private static async Task<string> CreateOrders()
        {
            var creator = new OrdersCreator(ApiUrl, new ConsoleLoggerWriter());
            return await creator.CreateOrder()
                .WithItem("Iphone Xs", "Brand new Iphone", 10)
                .WithItem("Iphone Xs Max", "Brand new Iphone", 3)
                .WithItem("Samsung Galaxy 9", "Brand new Samsung", 5)
                .Build();
        }
    }

    internal class ConsoleLoggerWriter : ILoggerWriter
    {
        public void Debug(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string message, Exception exception)
        {
            Console.WriteLine(message);
            Console.WriteLine($"Exception: {exception}");

        }
    }
}
