using Checkout.DataAccess.InMemory.Entities;
using Microsoft.EntityFrameworkCore;

namespace Checkout.DataAccess.InMemory
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options)
            :base(options)
        {   
        }

        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }
    }
}