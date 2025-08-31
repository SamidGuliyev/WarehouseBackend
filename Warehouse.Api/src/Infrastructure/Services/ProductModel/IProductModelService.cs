using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.src.Infrastructure.Services.ProductModel;

public interface IProductModelService
{
    Task AddProductModelAsync(AddProductModelDto dto);
    IQueryable<GetProductColorDto> GetAllProductModel(string? filter = null);
    GetProductModelDto? GetProductModelById(int id);
    void DeleteProductModel(int id);
}