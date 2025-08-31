using Warehouse.Api.src.Domain.Entities;
using Warehouse.Api.src.Persistence.Repositories;

namespace Warehouse.Api.Persistence.ProductModels;

public interface IProductModelRepository : IGenericRepository<ProductModelName, int>
{
    
}