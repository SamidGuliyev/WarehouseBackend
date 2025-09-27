using System.Text.Json.Serialization;
using Warehouse.Api.src.Domain.Entities;

namespace Warehouse.Api.src.Infrastructure.DTOs;

public record AddProductDto(
    string Size,
    float Price,
    int ColorId,
    int ProductModelId,
    int BlockNumber,
    int PieceNumber,
    int Stock,
    string? Thumbnail
);


public record struct ProductDto(int Id, string Size, float? Price, string? Thumbnail,
int ColorId, int ProductModelId, int BlockNumber, int PieceNumber, int Stock, [property: JsonPropertyName("created_at")] DateTime CreatedAt);

public record struct ProductJoinDto(
    int Id,
    string Size,
    float? Price,
    string? Thumbnail,
    int ColorId,
    int ProductModelId,
    int BlockNumber,
    int PieceNumber,
    int Stock,
    Guid UserId,
    [property: JsonPropertyName("created_at")] DateTime CreatedAt,
    [property: JsonPropertyName("updated_at")] DateTime UpdatedAt,
    [property: JsonPropertyName("product_color_name")] string ProductColorName,
    [property: JsonPropertyName("product_model_name")] string ProductModelNameName
);

public record struct ProductJoinStockStringDto(
    int Id,
    string Size,
    float? Price,
    string? Thumbnail,
    int ColorId,
    int ProductModelId,
    int BlockNumber,
    int PieceNumber,
    string Stock,
    Guid UserId,
    [property: JsonPropertyName("created_at")] DateTime CreatedAt,
    [property: JsonPropertyName("updated_at")] DateTime UpdatedAt,
    [property: JsonPropertyName("product_color_name")] string ProductColorName,
    [property: JsonPropertyName("product_model_name")] string ProductModelNameName
);

public record UpdateProductDto(
    int Id,
    string? Size,
    float Price,
    IFormFile? Thumbnail,
    string? OldThumbnailUrl,
    int? BlockNumber,
    int? PieceNumber,
    int? Stock
);



public record struct GetProductItem(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("quantity")] short Quantity,
    [property: JsonPropertyName("price")] float Price,
    [property: JsonPropertyName("unit")] ProductUnit Unit,
    string Size,
    string Thumbnail,
    string ColorName,
    string ModelName
);

public record struct ProductItem(
    [property: JsonPropertyName("id")] 
    int Id,
    
    [property: JsonPropertyName("quantity")]
    short Quantity,
    
    [property: JsonPropertyName("price")]
    float Price,
    
    [property: JsonPropertyName("unit")]
    ProductUnit Unit
);