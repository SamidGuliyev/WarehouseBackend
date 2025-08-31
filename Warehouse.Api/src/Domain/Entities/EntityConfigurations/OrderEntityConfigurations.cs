using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.src.Domain.Entities.EntityConfigurations;

public sealed class OrderEntityConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.ToTable("orders");
        entity.HasKey(p => p.Id);
        entity.HasIndex(p => p.CustomerId);
        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.CustomerId).HasColumnName("customer_id");
        entity.Property(e => e.Status).HasColumnName("status");
        entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
        entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        entity.Property(e => e.ProductItems).HasColumnName("product_items").HasColumnType("jsonb")
            .HasConversion(v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
            v => JsonSerializer.Deserialize<IEnumerable<ProductItem>>(v, JsonSerializerOptions.Default)!);
        
        entity.HasOne(e => e.Customer)
            .WithMany(e => e.Orders)
            .HasForeignKey(e => e.CustomerId)
            .HasConstraintName("fk_orders_customers_user_id")
            .OnDelete(DeleteBehavior.Restrict);
    }
}