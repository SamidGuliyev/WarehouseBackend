namespace Warehouse.Api.src.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public required string Fullname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    public IEnumerable<Product> Products { get; set; } = [];
}
