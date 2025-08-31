namespace Warehouse.Api.src.Domain.Entities;

public sealed class ProductModelName : BaseEntity<int>
{
    public required string Name { get; set; }
    public IEnumerable<Product> Products { get; set; }

}
