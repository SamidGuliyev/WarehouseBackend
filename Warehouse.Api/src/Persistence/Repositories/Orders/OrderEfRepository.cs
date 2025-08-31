using Microsoft.EntityFrameworkCore;
using Warehouse.Api.src.Domain;
using Warehouse.Api.src.Domain.Entities;
using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.src.Persistence.Repositories.Orders;

public sealed class OrderEfRepository(BaseDbContext db) : GenericEfRepository<Order, long>(db), IOrderRepository
{
    public async Task CompleteOrder(long id)
    {
        var order = GetById(id);
        if (order is null || order.IsDeleted) throw new NullReferenceException("Order not found");
        if (order.Status == OrderStatus.Completed) throw new ArgumentException("Order is already completed");
        try
        {
            order.Status = OrderStatus.Completed;

            var productList = order.ProductItems.ToList();

            var products = db.Products
                .Where(p => productList.Select(pi => pi.Id).Contains(p.Id));

            foreach (var item in products.Include(p => p.ProductModelName).Include(p => p.ProductColor))
            {
                var productItem = productList.Find(p => p.Id == item.Id);
                if(item.IsDeleted) throw new NullReferenceException("Product item not found");

                switch (productItem)
                {
                    case { Unit: ProductUnit.Block }:
                        if (item.Stock < productItem.Quantity * item.PieceNumber)
                        {
                            throw new ArgumentOutOfRangeException(
                                $"{item.ProductModelName.Name}-{item.Size}-{item.ProductColor.Name}", "Out of stock");
                        }

                        item.Stock -= productItem.Quantity * item.PieceNumber;
                        break;
                    case { Unit: ProductUnit.Box }:
                        if (item.Stock < productItem.Quantity * item.BlockNumber * item.PieceNumber)
                        {
                            throw new ArgumentOutOfRangeException(
                                $"{item.ProductModelName.Name}-{item.Size}-{item.ProductColor.Name}", "Out of stock");
                        }

                        item.Stock -= productItem.Quantity * item.BlockNumber * item.PieceNumber;
                        break;
                }
            }

            await db.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException(e.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public IEnumerable<GetOrderDto> GetOrdersAsync(string? filter = null)
    {
        var orders = db.Orders.AsNoTracking()
            .Include(o => o.Customer).AsQueryable();

        if (filter == "is_deleted") orders = orders.Where(o => o.IsDeleted);
        else if (string.IsNullOrEmpty(filter)) orders = orders.Where(o => !o.IsDeleted);
        

        var result = orders.ToList().Select(order =>
        {
            var getProductItem = order.ProductItems.Select(pi =>
            {
                var product = db.Products.AsNoTracking()
                    .Include(mp => mp.ProductModelName)
                    .Include(cp => cp.ProductColor).FirstOrDefault();
    
                return new GetProductItem(
                    Id: pi.Id,
                    Price: pi.Price,
                    Quantity: pi.Quantity,
                    Unit: pi.Unit,
                    Size: product?.Size ?? string.Empty,
                    Thumbnail:product?.Thumbnail ?? string.Empty,
                    ColorName: product?.ProductColor.Name ?? string.Empty,
                   ModelName:product?.ProductModelName.Name ?? string.Empty
                );
            });

            return new GetOrderDto(
                Id: order.Id,
                CustomerName: order.Customer.Name, // öz property-nə görə düzəlt
                Status: order.Status,
                GetProductItems: getProductItem
            );
        });

        return result;
    }

}
