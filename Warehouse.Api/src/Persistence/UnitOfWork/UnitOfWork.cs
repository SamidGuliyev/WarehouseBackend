using Warehouse.Api.Persistence.ProductModels;
using Warehouse.Api.src.Domain;
using Warehouse.Api.src.Persistence.Repositories.Auth;
using Warehouse.Api.src.Persistence.Repositories.Customers;
using Warehouse.Api.src.Persistence.Repositories.Orders;
using Warehouse.Api.src.Persistence.Repositories.ProductColors;
using Warehouse.Api.src.Persistence.Repositories.Products;

namespace Warehouse.Api.src.Persistence.UnitOfWork;

public sealed class UnitOfWork(BaseDbContext db) : IUnitOfWork
{
    public IProductRepository ProductRepository => new ProductEfRepository(db);
    public IOrderRepository OrderRepository => new OrderEfRepository(db);

    public ICustomerRepository CustomerRepository => new CustomerEfRepository(db);
    public IAuthRepository AuthRepository => new AuthEfRepository(db);
    public IProductColorRepository ProductColorRepository => new ProductColorEfRepository(db);
    public IProductModelRepository ProductModelRepository => new ProductModelEfRepository(db);
    public void Save() => db.SaveChanges();
    public async Task SaveAsync() => await db.SaveChangesAsync();
}
