using System;
using System.Linq;
using System.Threading.Tasks;
using Checkout.DataAccess.InMemory;
using Checkout.DataAccess.InMemory.Entities;
using Checkout.Domain.Requests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Checkout.UnitTests.Checkout.DataAccess.InMemory
{
    public class InMemoryOrdersRepositoryTest
    {
        private const ulong UserId = 234;

        [Fact]
        public async Task CreateOrder()
        {
            await RunInContext(nameof(CreateOrder), async (repository, context) =>
            {
                //act
                var orderId = await repository.CreateOrderForUser(UserId);

                //assert
                var dbOrder = await context.Orders.FirstAsync();
                Assert.Equal(dbOrder.Id, orderId);
            });
        }

        [Fact]
        public async Task GetOrderById()
        {
            await RunInContext(nameof(GetOrderById), async (repository, context) =>
            {
                //arrange
                var orderId = "orderId";
                string orderItemId = "order item Id";
                string name = "name";
                string description = "desc";
                int quantity = 33;
                await context.Orders.AddAsync(new OrderEntity { UserId = UserId, Id = orderId });
                await context.OrderItems.AddAsync(new OrderItemEntity
                    {OrderId = orderId, Id = orderItemId, Name = name, Quantity = quantity, Description = description});
                await context.SaveChangesAsync();

                //act
                var foundOrder = await repository.GetOrderById(UserId, orderId);

                //assert
                var dbOrder = await context.Orders.FirstAsync();
                Assert.NotNull(foundOrder);
                Assert.Equal(dbOrder.Id, foundOrder.Id);
                Assert.Collection(foundOrder.OrderItems, model =>
                {
                    Assert.Equal(model.Id, orderItemId);
                    Assert.Equal(model.Name, name);
                    Assert.Equal(model.Description, description);
                    Assert.Equal(model.Quantity, quantity);
                });
            });
        }

        [Fact]
        public async Task GetAllOrdersForUser()
        {
            await RunInContext(nameof(GetAllOrdersForUser), async (repository, context) =>
            {
                //arrange
                var orderEntity = new OrderEntity { UserId = UserId, Id = "orderId" };
                var orderEntity2 = new OrderEntity { UserId = UserId, Id = "orderId2" };

                var orderItemEntity = new OrderItemEntity
                {
                    OrderId = "orderId",
                    Id = "order item Id",
                    Name = "name",
                    Quantity = 33, Description = "desc"
                };

                var orderItemEntity2 = new OrderItemEntity
                {
                    OrderId = "orderId2",
                    Id = "order item Id2",
                    Name = "name2",
                    Quantity = 332,
                    Description = "desc2"
                };

                await context.Orders.AddAsync(orderEntity);
                await context.Orders.AddAsync(orderEntity2);
                await context.OrderItems.AddAsync(orderItemEntity);
                await context.OrderItems.AddAsync(orderItemEntity2);
                await context.SaveChangesAsync();

                //act
                var foundOrders = await repository.GetAllOrdersForUser(UserId);

                //assert
                Assert.Collection(foundOrders,
                    order =>
                    {
                        Assert.Equal(orderEntity.Id, order.Id);
                        Assert.Equal(UserId, order.UserId);
                        Assert.Collection(order.OrderItems, item =>
                        {
                            Assert.Equal(orderItemEntity.Id, item.Id);
                            Assert.Equal(orderItemEntity.Name, item.Name);
                            Assert.Equal(orderItemEntity.Description, item.Description);
                            Assert.Equal(orderItemEntity.Quantity, item.Quantity);
                        });
                    },
                    order =>
                    {
                        Assert.Equal(orderEntity2.Id, order.Id);
                        Assert.Equal(UserId, order.UserId);
                        Assert.Collection(order.OrderItems, item =>
                        {
                            Assert.Equal(orderItemEntity2.Id, item.Id);
                            Assert.Equal(orderItemEntity2.Name, item.Name);
                            Assert.Equal(orderItemEntity2.Description, item.Description);
                            Assert.Equal(orderItemEntity2.Quantity, item.Quantity);
                        });
                    }

                    );
            });
        }

        [Fact]
        public async Task AddItemToOrder()
        {
            await RunInContext(nameof(AddItemToOrder), async (repository, context) =>
            {
                //arrange
                var orderId = "orderId";
                string name = "name";
                string description = "description";
                int quantity = 10;

                await context.Orders.AddAsync(new OrderEntity { UserId = UserId, Id = orderId });
                await context.SaveChangesAsync();
                var request = new CreateOrderItemRequest { Description = description, Name = name, Quantity = quantity };

                //act
                var addedItem = await repository.AddItemToOrder(UserId, orderId, request);

                //assert
                var dbOrderItem = await context.OrderItems.FirstAsync();
                Assert.Equal(dbOrderItem.Id, addedItem);
                Assert.Equal(dbOrderItem.Name, name);
                Assert.Equal(dbOrderItem.Description, description);
                Assert.Equal(dbOrderItem.Quantity, quantity);
            });
        }


        [Fact]
        public async Task RemoveItemFromOrder()
        {
            await RunInContext(nameof(RemoveItemFromOrder), async (repository, context) =>
            {
                //arrange
                var orderId = "orderId";
                var orderItemId = "order item Id";

                await context.Orders.AddAsync(new OrderEntity { UserId = UserId, Id = orderId });
                await context.OrderItems.AddAsync(new OrderItemEntity { OrderId = orderId, Id = orderItemId });
                await context.SaveChangesAsync();

                //act
                await repository.RemoveItemFromOrder(UserId, orderId, orderItemId);

                //assert
                var dbOrderItem = await context.OrderItems.FirstOrDefaultAsync();
                Assert.Null(dbOrderItem);
            });
        }

        [Fact]
        public async Task ChangeOrderItemQuantity()
        {
            await RunInContext(nameof(ChangeOrderItemQuantity), async (repository, context) =>
            {
                //arrange
                var orderId = "orderId";
                var orderItemId = "order item Id";

                await context.Orders.AddAsync(new OrderEntity { UserId = UserId, Id = orderId });
                await context.OrderItems.AddAsync(new OrderItemEntity { OrderId = orderId, Id = orderItemId, Quantity = 50});
                await context.SaveChangesAsync();

                //act
                await repository.ChangeOrderItemQuantity(UserId, orderId, orderItemId, 10);

                //assert
                var dbOrderItem = await context.OrderItems.FirstOrDefaultAsync();
                Assert.Equal(10, dbOrderItem.Quantity);
            });
        }

        [Fact]
        public async Task ChangeOrderItemQuantityThatLessThanZero()
        {
            await RunInContext(nameof(ChangeOrderItemQuantityThatLessThanZero), async (repository, context) =>
            {
                //arrange
                var orderId = "orderId";
                var orderItemId = "order item Id";

                await context.Orders.AddAsync(new OrderEntity { UserId = UserId, Id = orderId });
                await context.OrderItems.AddAsync(new OrderItemEntity { OrderId = orderId, Id = orderItemId, Quantity = 50 });
                await context.SaveChangesAsync();

                //act & assert
                await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                    repository.ChangeOrderItemQuantity(UserId, orderId, orderItemId, -10));                
            });
        }

        [Fact]
        public async Task PurgeOrder()
        {
            await RunInContext(nameof(PurgeOrder), async (repository, context) =>
            {
                //arrange
                var orderId = "orderId";

                await context.Orders.AddAsync(new OrderEntity { UserId = UserId, Id = orderId });
                await context.OrderItems.AddAsync(new OrderItemEntity { OrderId = orderId, Id = "order item Id", Quantity = 50 });
                await context.SaveChangesAsync();

                //act
                await repository.PurgeOrder(UserId, orderId);

                //assert
                var dbOrderItem = await context.OrderItems.FirstOrDefaultAsync();
                Assert.Null(dbOrderItem);
            });
        }



        private static async Task RunInContext(string dbName, Func<InMemoryOrdersRepository, OrdersContext, Task> testTask)
        {
            var options = new DbContextOptionsBuilder<OrdersContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            using (var context = new OrdersContext(options))
            {

                var repository = new InMemoryOrdersRepository(context);
                await testTask(repository, context);
            }
        }
    }
}