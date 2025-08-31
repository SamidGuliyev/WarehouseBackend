using Warehouse.Api.src.Domain;
using Warehouse.Api.src.Domain.Entities;

namespace Warehouse.Api.src.Persistence.Repositories.ProductColors;

public sealed class ProductColorEfRepository(BaseDbContext db): GenericEfRepository<ProductColor, int>(db), IProductColorRepository
{
    
}