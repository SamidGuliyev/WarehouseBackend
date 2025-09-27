using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Warehouse.Api.src.Domain.Entities.EntityConfigurations;

public sealed class ProductEntityConfigrations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> data)
    {
        data.ToTable("products");
        data.HasKey(e => e.Id);
        data.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd(); // auto-increment etnek ucun
        data.Property(e => e.Size).HasColumnName("size").HasMaxLength(200);
        data.Property(e => e.IsDeleted).HasColumnName("is_deleted");
        data.Property(e => e.ColorId).HasColumnName("color_id");
        data.Property(e => e.UserId).HasColumnName("user_id");
        data.Property(e => e.Thumbnail).HasColumnName("thumbnail");
        data.Property(e => e.BlockNumber).HasColumnName("block_number");
        data.Property(e => e.PieceNumber).HasColumnName("piece_number");
        data.Property(e => e.ProductModelId).HasColumnName("product_model_id");
        data.Property(e => e.Stock).HasColumnName("stock");
        data.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        data.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        data.Property(e => e.Price).HasColumnName("price");
        data.HasIndex(p => new { p.Size, p.ColorId, p.ProductModelId })
            .IsUnique();

        data.HasOne(e => e.User)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.UserId)
            .HasConstraintName("fk_products_users_user_id")
            .OnDelete(DeleteBehavior.Restrict);

        data.HasOne(e => e.ProductModelName)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.ProductModelId)
            .HasConstraintName("fk_products_product_models_names_product_model_id")
            .OnDelete(DeleteBehavior.Restrict);

        data.HasOne(e => e.ProductColor)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.ColorId)
            .HasConstraintName("fk_products_product_colors_product_color_id")
            .OnDelete(DeleteBehavior.Restrict);
    }
}