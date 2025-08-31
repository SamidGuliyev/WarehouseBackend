using Warehouse.Api.Persistence.ProductModels;
using Warehouse.Api.src.Persistence.Repositories.Auth;
using Warehouse.Api.src.Persistence.Repositories.Customers;
using Warehouse.Api.src.Persistence.Repositories.Orders;
using Warehouse.Api.src.Persistence.Repositories.ProductColors;
using Warehouse.Api.src.Persistence.Repositories.Products;

namespace Warehouse.Api.src.Persistence.UnitOfWork;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    IOrderRepository OrderRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IAuthRepository AuthRepository { get; }
    IProductColorRepository ProductColorRepository { get; }
    IProductModelRepository ProductModelRepository { get; }
    void Save();
    Task SaveAsync();
}
