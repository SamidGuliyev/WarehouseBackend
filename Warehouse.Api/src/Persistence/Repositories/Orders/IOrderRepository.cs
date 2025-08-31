using System.Linq.Expressions;
using Warehouse.Api.src.Domain.Entities;
using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.src.Persistence.Repositories.Orders;

public interface IOrderRepository : IGenericRepository<Order, long>
{
   Task CompleteOrder(long id);
   IEnumerable<GetOrderDto> GetOrdersAsync(string? filter = null);
}
