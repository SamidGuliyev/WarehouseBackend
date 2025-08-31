using Warehouse.Api.src.Domain.Entities;

namespace Warehouse.Api.src.Infrastructure.DTOs;

public record struct AddOrderDto(int CustomerId,  
    IEnumerable<ProductItem> ProductItems);
public record struct GetOrderDto(
    long Id, 
    string CustomerName,
    OrderStatus Status, 
    IEnumerable<GetProductItem> GetProductItems);

public record struct AddOrderResponseDto(bool Success,List<string>?Errors=null);


    
public record struct OrderWithProductDto(int OrderId, string ProductName, short Quantity, float Price, string ProductColor);
    
    
    