using InventorySystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.Schemas;

public class ProductSchema : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.ToTable("products");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        entity.Property(x => x.Sku)
            .HasColumnName("sku")
            .HasColumnType("varchar(50)")
            .IsRequired();

        entity.Property(x => x.Stock)
            .HasColumnName("stock")
            .IsRequired();

        entity.Property(x => x.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        entity.Property(x => x.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();

        entity.Property(x => x.SearchVerctor)
            .HasColumnName("search_vector");

        entity.Property(x => x.DeletedAt)
            .HasColumnName("deleted_at")
            .HasColumnType("timestamp with time zone");

        entity.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        entity.HasOne(x => x.Category)
        .WithMany(x => x.Products)
        .HasForeignKey(x => x.CategoryId)
        .OnDelete(DeleteBehavior.Restrict);

        entity.HasGeneratedTsVectorColumn(
            x => x.SearchVerctor,
            "english",
            x => new { x.Name, x.Sku }
        );

        entity.HasIndex(x => x.Sku).IsUnique();
        entity.HasIndex(x => x.CreatedAt);
        entity.HasIndex(x => x.DeletedAt);
        entity.HasIndex(x => x.SearchVerctor).HasMethod("GIN");

        entity.HasQueryFilter(x => x.DeletedAt == null);
    }
}