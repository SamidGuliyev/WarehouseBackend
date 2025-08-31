using Warehouse.Api.src.Domain.Entities;
using System.Linq.Expressions;

namespace Warehouse.Api.src.Persistence.Repositories.Products;

public interface IProductRepository : IGenericRepository<Product, int>
{
    IEnumerable<Product> GetProductsWithJoin(Expression<Func<Product, bool>>? filter = null);
}
