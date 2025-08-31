using Microsoft.EntityFrameworkCore;
using Warehouse.Api.src.Domain.Entities;
using Warehouse.Api.src.Infrastructure.DTOs;
using Warehouse.Api.src.Persistence.UnitOfWork;

namespace Warehouse.Api.src.Infrastructure.Services.ProductModel;

public sealed class ProductModelService(IUnitOfWork unitOfWork) : IProductModelService
{
    private IProductModelService _productModelServiceImplementation;

    public async Task AddProductModelAsync(AddProductModelDto dto)
    {
        var productModel = new ProductModelName
        {
            Name = dto.Name
        };
        try
        {
            unitOfWork.ProductModelRepository.Add(productModel);
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

    public IQueryable<GetProductColorDto> GetAllProductModel(string? filter = null)
    {
        var productModel = unitOfWork.ProductModelRepository.GetAll(c => !c.IsDeleted);
        switch (filter)
        {
            case "all":
                productModel = unitOfWork.ProductModelRepository.GetAll();
                break;
            case "is_deleted":
                productModel = unitOfWork.ProductModelRepository.GetAll(c => c.IsDeleted);
                break;
        }
        return 
            productModel.Select(c => new GetProductColorDto(c.Id, c.Name));
    }


    public GetProductModelDto? GetProductModelById(int id)
    {
        var productModel =  unitOfWork.ProductModelRepository.GetById(id);
        if (productModel == null) throw new NullReferenceException();
        return 
            new GetProductModelDto(productModel.Id, productModel.Name);
    }

    public void DeleteProductModel(int id)
    {
        try
        {
            unitOfWork.ProductModelRepository.Delete(id);
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