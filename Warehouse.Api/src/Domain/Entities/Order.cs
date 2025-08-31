using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.src.Domain.Entities;

public sealed class Order : BaseEntity<long>
{
    public required int CustomerId { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<ProductItem> ProductItems { get; set; } = [];
    public Customer Customer { get; init; }
}

