using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.src.Infrastructure.Services.ProductColors;

public interface IProductColorService
{
    Task AddProductColorAsync(AddProductColorDto dto);
    IEnumerable<GetProductColorDto> GetAllProductColor(string? filter = null);
    GetProductColorDto? GetProductColorById(int id);
    void DeleteProductColor(int id);
}