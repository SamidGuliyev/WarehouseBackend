namespace Warehouse.Api.src.Domain.Entities
{
    public sealed class Customer : BaseEntity<int>
    {
        public required string Name { get; set; }
        public string? Phone { get; set; }
        public IEnumerable<Order> Orders { get; init; }
    }
}
