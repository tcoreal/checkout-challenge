using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.BusinessCommon;
using Checkout.DataAccess.InMemory.Entities;
using Checkout.DataAccess.InMemory.Extensions;
using Checkout.Domain.Exceptions;
using Checkout.Domain.Models;
using Checkout.Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace Checkout.DataAccess.InMemory
{
    public class InMemoryOrdersRepository : IOrdersRepository
    {
        private readonly OrdersContext _ordersContext;

        public InMemoryOrdersRepository(OrdersContext ordersContext)
        {
            _ordersContext = ordersContext;
        }

        public async Task<IEnumerable<OrderModel>> GetAllOrdersForUser(ulong userId)
        {
            var data = await (from order in _ordersContext.Orders
                    where order.UserId == userId
                    join item in _ordersContext.OrderItems on order.Id equals item.OrderId into orderItem
                    from oi in orderItem.DefaultIfEmpty()
                    select new OrderAndItem(order, oi))
                .ToArrayAsync();

            return GroupItemsByOrder(data).ToArray();
        }

        public async Task<OrderModel> GetOrderById(ulong userId, string orderId)
        {
            var data = await (from order in _ordersContext.Orders
                    where order.UserId == userId && order.Id == orderId
                    join item in _ordersContext.OrderItems on order.Id equals item.OrderId into orderItem
                    from oi in orderItem.DefaultIfEmpty()
                    select new OrderAndItem(order, oi))
                .ToArrayAsync();

            return GroupItemsByOrder(data).FirstOrDefault();
        }

        public async Task<string> CreateOrderForUser(ulong userId)
        {
            var orderId = Guid.NewGuid().ToString();
            var newOrder = new OrderEntity { UserId = userId, Id = orderId };
            await _ordersContext.Orders.AddAsync(newOrder);
            await _ordersContext.SaveChangesAsync();
            return orderId;
        }

        public async Task AddItemToOrder(ulong userId, string orderId, CreateOrderItemRequest request)
        {
            var order = await GetOrder(userId, orderId);
            var newOrderItem = new OrderItemEntity
            {
                Id = Guid.NewGuid().ToString(),
                OrderId = order.Id,
                Name = request.Name,
                Description = request.Description,
                Quantity = request.Quantity
            };
            await _ordersContext.OrderItems.AddAsync(newOrderItem);
            await _ordersContext.SaveChangesAsync();
        }

        public async Task ChangeOrderItemQuantity(ulong userId, string orderId, string orderItemId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity should be greater that 0");
            }

            var order = await GetOrder(userId, orderId);

            var item = await _ordersContext.OrderItems.FirstOrDefaultAsync(x =>
                x.Id == orderItemId && x.OrderId == order.Id);
            if (item == null)
            {
                throw new OrderItemNotFoundException(orderItemId, orderId);
            }

            item.Quantity = quantity;
            _ordersContext.OrderItems.Update(item);
            await _ordersContext.SaveChangesAsync();
        }

        public async Task RemoveItemFromOrder(ulong userId, string orderId, string orderItemId)
        {
            var order = await GetOrder(userId, orderId);
            var item = await _ordersContext.OrderItems.FirstOrDefaultAsync(x =>
                x.Id == orderItemId && x.OrderId == order.Id);
            if (item == null)
            {
                return;
            }

            _ordersContext.OrderItems.Remove(item);
            await _ordersContext.SaveChangesAsync();
        }

        public async Task PurgeOrder(ulong userId, string orderId)
        {
            var order = await GetOrder(userId, orderId);
            var itemsToRemove = await _ordersContext.OrderItems.Where(x => x.OrderId == order.Id).ToArrayAsync();
            _ordersContext.OrderItems.RemoveRange(itemsToRemove);
            await _ordersContext.SaveChangesAsync();
        }

        private static IEnumerable<OrderModel> GroupItemsByOrder(OrderAndItem[] data) =>
            data.GroupBy(x => new {x.Order.Id, x.Order.UserId})
                .Select(x => new OrderModel
                {
                    Id = x.Key.Id,
                    UserId = x.Key.UserId,
                    OrderItems = x.Select(_ => _.Item?.ToModel()).Where(_ => _ != null)
                });

        private async Task<OrderEntity> GetOrder(ulong userId, string orderId)
        {
            var order = await _ordersContext.Orders.Where(x => x.Id == orderId && x.UserId == userId)
                .FirstOrDefaultAsync();
            if (order == null)
            {
                throw new OrderNotFoundException(orderId, userId);
            }

            return order;
        }

        private class OrderAndItem
        {
            public OrderEntity Order { get; }
            public OrderItemEntity Item { get; }

            public OrderAndItem(OrderEntity order, OrderItemEntity item)
            {
                Order = order;
                Item = item;
            }
        }
    }
}
