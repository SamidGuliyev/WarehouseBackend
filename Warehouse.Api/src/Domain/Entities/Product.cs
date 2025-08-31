namespace Warehouse.Api.src.Domain.Entities;

public sealed class Product : BaseUpdatedEntity<int>
{
    public required string Size { get; set; }
    public float Price { get; set; }
    public string? Thumbnail { get; set; }
    public int Stock { get; set; }
    public required int BlockNumber { get; set; }
    public required int PieceNumber { get; set; }
    public int ColorId { get; init; }
    public int ProductModelId { get; init; }
    public Guid UserId { get; init; }

    public User User { get; set; }
    public ProductModelName ProductModelName { get; set; } 
    public ProductColor ProductColor { get; set; }


}