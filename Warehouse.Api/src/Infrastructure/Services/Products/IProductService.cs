using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.src.Infrastructure.Services.Products;

public interface IProductService
{
    Task AddProductAsync(AddProductDto dto, Guid userId);
    IEnumerable<ProductDto> GetAllProducts(string? filter = null);
    ProductDto? GetProductById(int id); // sorus bunu!!!
    IEnumerable<ProductJoinDto> GetProductJoin(string? filter = null);
    void HardDelete(int id);
    Task UpdateProductAsync(UpdateProductDto dto);
    void SoftDelete(int id);
    
}
