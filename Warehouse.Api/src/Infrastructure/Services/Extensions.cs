using Warehouse.Api.src.Infrastructure.Providers.Services;
using Warehouse.Api.src.Infrastructure.Services.Auth;
using Warehouse.Api.src.Infrastructure.Services.Customers;
using Warehouse.Api.src.Infrastructure.Services.Orders;
using Warehouse.Api.src.Infrastructure.Services.ProductColors;
using Warehouse.Api.src.Infrastructure.Services.ProductModel;
using Warehouse.Api.src.Infrastructure.Services.Products;

namespace Warehouse.Api.src.Infrastructure.Services;

public static class Extensions
{
    public static void AddInfrastructureScopeResolvers(this IServiceCollection services)
    {
        services.AddScoped<TokenService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductColorService, ProductColorService>();
        services.AddScoped<IProductModelService, ProductModelService>();
    }
}
