using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Warehouse.Api.src.Domain;
using Warehouse.Api.src.Domain.Entities;

namespace Warehouse.Api.src.Persistence.Repositories.Products;

public sealed class ProductEfRepository(BaseDbContext db) : GenericEfRepository<Product, int>(db), IProductRepository
{
    public IEnumerable<Product> GetProductsWithJoin(Expression<Func<Product, bool>>? filter = null)
    {
        var obj = db.Products.Include(o => o.ProductColor).Include(o => o.ProductModelName).Select(p => new Product
        {
            BlockNumber = p.BlockNumber,
            PieceNumber = p.PieceNumber,
            Size = p.Size,
            Price = p.Price,
            ColorId = p.ColorId,
            ProductModelId = p.ProductModelId,
            Thumbnail = p.Thumbnail,
            Stock = p.Stock,
            UserId = p.UserId,
            Id = p.Id,
            IsDeleted = p.IsDeleted,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            ProductColor = new ProductColor
            {
                Name = p.ProductColor.Name, Id = p.ProductColor.Id, CreatedAt = p.ProductColor.CreatedAt,
                IsDeleted = p.ProductColor.IsDeleted
            },
            ProductModelName = new ProductModelName
            {
                Name = p.ProductModelName.Name, Id = p.ProductModelName.Id, CreatedAt = p.ProductModelName.CreatedAt,
                IsDeleted = p.ProductModelName.IsDeleted
            },
        });
        return filter != null ? obj.Where(filter) : obj;
    }
    
    public IEnumerable<Product> GetProductByName(string name)
    {
        return db.Products.AsNoTracking()
            .Include(o => o.ProductModelName)
            .Include(o => o.ProductColor)
            .Where(p => (p.Size.Contains(name) || p.ProductModelName.Name.Contains(name) || p.ProductColor.Name.ToLower().Contains(name)) && !p.IsDeleted);
    
       
    }
}
