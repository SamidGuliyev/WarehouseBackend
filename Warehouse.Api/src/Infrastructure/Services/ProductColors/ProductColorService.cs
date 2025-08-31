using Microsoft.EntityFrameworkCore;
using Warehouse.Api.src.Domain.Entities;
using Warehouse.Api.src.Infrastructure.DTOs;
using Warehouse.Api.src.Persistence.UnitOfWork;

namespace Warehouse.Api.src.Infrastructure.Services.ProductColors;

public sealed class ProductColorService(IUnitOfWork unitOfWork) : IProductColorService
{
    public async Task AddProductColorAsync(AddProductColorDto dto)
    {
        var productColor = new ProductColor
        {
            Name = dto.Name
        };
        try
        {
            unitOfWork.ProductColorRepository.Add(productColor);
            await unitOfWork.SaveAsync();
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

    public IEnumerable<GetProductColorDto> GetAllProductColor(string? filter = null)
    {
        IEnumerable<ProductColor> productColor = unitOfWork.ProductColorRepository.GetAll(c => !c.IsDeleted);
        switch (filter)
        {
            case "all":
                productColor = unitOfWork.ProductColorRepository.GetAll();
                break;
            case "is_deleted":
                productColor = unitOfWork.ProductColorRepository.GetAll(c => c.IsDeleted);
                break;
        }
        return 
            productColor.Select(c => new GetProductColorDto(c.Id, c.Name));
    }

    public GetProductColorDto? GetProductColorById(int id)
    {
        var productColor =  unitOfWork.ProductColorRepository.GetById(id);
        if (productColor == null) throw new NullReferenceException();
        return 
            new GetProductColorDto(productColor.Id, productColor.Name);
    }

    public void DeleteProductColor(int id)
    {
        try
        {
            unitOfWork.ProductColorRepository.Delete(id);
            unitOfWork.Save();
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
}