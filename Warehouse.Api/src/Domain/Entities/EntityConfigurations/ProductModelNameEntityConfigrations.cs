using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Warehouse.Api.src.Domain.Entities.EntityConfigurations;

public sealed class ProductModelNameEntityConfigrations : IEntityTypeConfiguration<ProductModelName>
{
    public void Configure(EntityTypeBuilder<ProductModelName> data)
    {
        data.ToTable("product_model_names");
        data.HasKey(e => e.Id);
        data.HasIndex(e => e.Name).IsUnique();
        data.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
        data.Property(d => d.Name).HasColumnName("model_name");
        data.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        data.Property(e => e.IsDeleted).HasColumnName("is_deleted");

        data.HasData([
            new ProductModelName {Id = 1, Name = "1296", CreatedAt = DateTime.UtcNow},
            new ProductModelName {Id = 2, Name = "5575", CreatedAt = DateTime.UtcNow},
            new ProductModelName {Id = 3, Name = "5175", CreatedAt = DateTime.UtcNow},
            new ProductModelName {Id = 4, Name = "708", CreatedAt = DateTime.UtcNow},
        ]);
    }
}