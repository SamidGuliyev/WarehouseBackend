using Microsoft.EntityFrameworkCore;
using Warehouse.Api.src.Domain.Entities;
using Warehouse.Api.src.Infrastructure.DTOs;
using Warehouse.Api.src.Persistence.UnitOfWork;

namespace Warehouse.Api.src.Infrastructure.Services.Products;

public sealed class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    public async Task AddProductAsync(AddProductDto dto, Guid userId)
    {
        try
        {
            string? filePath = null;

            if (dto.Thumbnail is not null)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "products");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                filePath = "uploads/products/" + Guid.CreateVersion7() + Path.GetExtension(dto.Thumbnail.FileName);

                await using var stream =
                    new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath), FileMode.Create);
                await dto.Thumbnail.CopyToAsync(stream);
            }
            

            var product = new Product
            {
                Size = dto.Size,
                Stock = dto.Stock,
                Price = dto.Price,
                ColorId = dto.ColorId,
                ProductModelId = dto.ProductModelId,
                BlockNumber = dto.BlockNumber,
                PieceNumber = dto.PieceNumber,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            if (filePath is not null && File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath)))
            {
                product.Thumbnail = filePath;
            }

            unitOfWork.ProductRepository.Add(product);
            await unitOfWork.SaveAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public IEnumerable<ProductDto> GetAllProducts(string? filter = null)
    {
        IEnumerable<Product> products = unitOfWork.ProductRepository.GetAll();

        if (filter == "is_deleted")
        {
            products = unitOfWork.ProductRepository.GetAll(p => !p.IsDeleted);
        }
        return
        products.Select(p => new ProductDto(p.Id, p.Size, p.Price, p.Thumbnail, p.ColorId, p.ProductModelId, p.BlockNumber, p.PieceNumber, p.Stock, p.CreatedAt));
    }

    public ProductDto? GetProductById(int id)
    {
        var product = unitOfWork.ProductRepository.GetById(id);
        if (product is null) return null;
        return new ProductDto
        {
            Id = product.Id,
            BlockNumber = product.BlockNumber,
            Price = product.Price,
            Thumbnail = product.Thumbnail,
            ColorId = product.ColorId,
            CreatedAt = product.CreatedAt,
            PieceNumber = product.PieceNumber,
            Stock = product.Stock,
            ProductModelId = product.ProductModelId,
            Size = product.Size
        };

    }

    public IEnumerable<ProductJoinDto> GetProductJoin(string? filter = null)
    {
        var products = unitOfWork.ProductRepository.GetProductsWithJoin(p => !p.IsDeleted);
        switch (filter)
        {
            case "is_deleted":
                products = unitOfWork.ProductRepository.GetProductsWithJoin(p => p.IsDeleted);
                break;
            case "all":
                products = unitOfWork.ProductRepository.GetProductsWithJoin();
                break;
        }

        return products.Select(p => new ProductJoinDto(
            p.Id,
            p.Size,
            p.Price,
            p.Thumbnail,
            p.ColorId,
            p.ProductModelId,
            p.BlockNumber,
            p.PieceNumber,
            p.Stock,
            p.UserId,
            p.CreatedAt,
            p.UpdatedAt,
            p.ProductColor.Name,
            p.ProductModelName.Name
        ));
    }

    public void HardDelete(int id)
    {
        var product = unitOfWork.ProductRepository.GetById(id);
        var fileName =  Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.Thumbnail);
        if (File.Exists(fileName)) File.Delete(fileName);
        unitOfWork.ProductRepository.Delete(id);
        unitOfWork.Save();
    }

    public void SoftDelete(int id)
    {
        var product = unitOfWork.ProductRepository.GetById(id);
        unitOfWork.ProductRepository.SoftDelete(product!);
        unitOfWork.Save();
    }

    public async Task UpdateProductAsync(UpdateProductDto dto) // bunu sorus dto null edib hem update hem de soft delete kimi istidafe etmek olar
    {
        try
        {
            var filePath = string.Empty;

            if (dto.Thumbnail is not null)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "products");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                if (!string.IsNullOrEmpty(dto.OldThumbnailUrl))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", dto.OldThumbnailUrl);
                    if (File.Exists(oldFilePath)) File.Delete(oldFilePath);
                }

                filePath = "uploads/products/" + Guid.CreateVersion7() + Path.GetExtension(dto.Thumbnail.FileName);

                await using var stream =
                    new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath), FileMode.Create);
                await dto.Thumbnail.CopyToAsync(stream);
            }
            else if (!string.IsNullOrEmpty(dto.OldThumbnailUrl) && dto.Thumbnail is null) filePath = null;
            
            await unitOfWork.ProductRepository.Update(new Product
            {
                Id = dto.Id,
                Size = dto.Size!,
                Price = dto.Price,
                Thumbnail = filePath,
                BlockNumber = dto.BlockNumber ?? 0,
                PieceNumber = dto.PieceNumber ?? 0,
                Stock = dto.Stock ?? 0,
                UpdatedAt = DateTime.UtcNow,
            });
            await unitOfWork.SaveAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
