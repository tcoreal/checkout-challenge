using System.Linq;
using Checkout.DataAccess.InMemory.Entities;
using Checkout.Domain;
using Checkout.Domain.Models;

namespace Checkout.DataAccess.InMemory.Extensions
{
    public static class MappingExtensions
    {
        public static OrderItemModel ToModel(this OrderItemEntity item) => new OrderItemModel
        {
            Id = item.Id,
            Description = item.Description,
            Name = item.Name,
            Quantity = item.Quantity
        };
    }
}