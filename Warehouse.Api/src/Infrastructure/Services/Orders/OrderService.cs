using Microsoft.EntityFrameworkCore;
using Warehouse.Api.src.Domain;
using Warehouse.Api.src.Domain.Entities;
using Warehouse.Api.src.Infrastructure.DTOs;
using Warehouse.Api.src.Persistence.UnitOfWork;

namespace Warehouse.Api.src.Infrastructure.Services.Orders;

public sealed class OrderService(IUnitOfWork unitOfWork, BaseDbContext db) : IOrderService
{
    public async Task<AddOrderResponseDto> AddOrder(AddOrderDto dto)
    {
        var productList = dto.ProductItems.ToList();
        var products = db.Products
            .Where(p => productList.Select(pi => pi.Id).Contains(p.Id));
        List<string> errorList = []; 
        foreach (var item in products.Include(p => p.ProductModelName).Include(p => p.ProductColor))
        {
            var productItem = productList.Find(p => p.Id == item.Id);
            if(item.IsDeleted) throw new NullReferenceException("Product item not found");
            if ((productItem.Unit == ProductUnit.Block && item.Stock < productItem.Quantity * item.PieceNumber) ||
                (productItem.Unit == ProductUnit.Box &&
                 item.Stock < productItem.Quantity * item.BlockNumber * item.PieceNumber))
                errorList.Add($"{item.ProductModelName.Name}-{item.Size}-{item.ProductColor.Name} is out of stock");
        }

        if (errorList.Count != 0)
        {
            return new AddOrderResponseDto(false, errorList);
        }
        var order = new Order
        {
            CustomerId = dto.CustomerId,
            Status = OrderStatus.Pending,
            ProductItems = dto.ProductItems
        };
        try
        {
            unitOfWork.OrderRepository.Add(order);
            await unitOfWork.SaveAsync();
            return new AddOrderResponseDto(true);
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

    public async Task CompleteOrder(long id)
    {
        await unitOfWork.OrderRepository.CompleteOrder(id);
    }

    public void Delete(long id)
    {
        var order = unitOfWork.OrderRepository.GetById(id) ?? throw new NullReferenceException();
        if(order.Status == OrderStatus.Completed) 
            throw new DbUpdateException("Completed order could not be deleted");
        unitOfWork.OrderRepository.SoftDelete(order);
        unitOfWork.Save();
    }

    public IEnumerable<GetOrderDto> GetOrders(string? filter = null)
    {
        return unitOfWork.OrderRepository.GetOrdersAsync(filter);
    }
}