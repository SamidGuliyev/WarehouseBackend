using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Warehouse.Api.src.Domain.Entities.EntityConfigurations;

public sealed class ProductColorEntityConfigrations : IEntityTypeConfiguration<ProductColor>
{
    public void Configure(EntityTypeBuilder<ProductColor> data)
    {
        data.ToTable("product_colors");
        data.HasKey(e => e.Id);
        data.HasIndex(e => e.Name).IsUnique();
        data.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
        data.Property(d => d.Name).HasColumnName("color_name");
        data.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        data.Property(e => e.IsDeleted).HasColumnName("is_deleted");

        data.HasData([
            new ProductColor {Id = 1, Name = "BK", CreatedAt = DateTime.UtcNow},
            new ProductColor {Id = 2, Name = "Golden", CreatedAt = DateTime.UtcNow},
            new ProductColor {Id = 3, Name = "White", CreatedAt = DateTime.UtcNow}
        ]);
    }
}