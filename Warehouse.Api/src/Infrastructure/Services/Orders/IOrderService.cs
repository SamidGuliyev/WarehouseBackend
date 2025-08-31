using Warehouse.Api.src.Domain.Entities;
using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.src.Infrastructure.Services.Orders;

public interface IOrderService
{
    public Task<AddOrderResponseDto> AddOrder(AddOrderDto dto);
    public Task CompleteOrder(long id);
    public void Delete(long id);
    public IEnumerable<GetOrderDto> GetOrders(string? filter = null);
}
