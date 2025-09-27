using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.src.Infrastructure.Services.Products;

public interface IProductService
{
    Task<string> UploadProductImage(IFormFile imagePath);
    Task AddProductAsync(AddProductDto dto, Guid userId);
    IEnumerable<ProductDto> GetAllProducts(string? filter = null);
    IEnumerable<ProductJoinStockStringDto> GetAllProductsByName(string name);
    ProductDto? GetProductById(int id); // sorus bunu!!!
    IEnumerable<ProductJoinDto> GetProductJoin(string? filter = null);
    void HardDelete(int id);
    Task UpdateProductAsync(UpdateProductDto dto);
    void SoftDelete(int id);
    
}
