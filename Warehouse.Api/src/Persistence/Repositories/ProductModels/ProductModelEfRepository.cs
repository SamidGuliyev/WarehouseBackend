using Warehouse.Api.src.Domain;
using Warehouse.Api.src.Domain.Entities;
using Warehouse.Api.src.Persistence.Repositories;

namespace Warehouse.Api.Persistence.ProductModels;

public sealed class ProductModelEfRepository(BaseDbContext db) : GenericEfRepository<ProductModelName, int>(db), IProductModelRepository
{
    
}