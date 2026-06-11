using InventorySystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.Schemas;

public class CategorySchema : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.ToTable("categories");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(50)")
            .IsRequired();

        entity.Property(x => x.DeletedAt)
            .HasColumnName("deleted_at")
            .HasColumnType("timestamp with time zone");

        entity.HasMany(x => x.Products)
        .WithOne(x => x.Category)
        .HasForeignKey(x => x.CategoryId);

        entity.HasIndex(x => x.DeletedAt);

        entity.HasQueryFilter(x => x.DeletedAt == null);
    }
}