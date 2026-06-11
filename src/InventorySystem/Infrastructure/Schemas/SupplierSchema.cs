using InventorySystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.Schemas;

public class SupplierSchema : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> entity)
    {
        entity.ToTable("suppliers");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Code)
            .HasColumnName("code")
            .HasColumnType("varchar(20)")
            .IsRequired();

        entity.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        entity.Property(x => x.Email)
            .HasColumnName("email")
            .HasColumnType("varchar(100)")
            .IsRequired();

        entity.Property(x => x.Phone)
            .HasColumnName("phone")
            .HasColumnType("varchar(20)");

        entity.Property(x => x.Address)
            .HasColumnName("address")
            .HasColumnType("text");

        entity.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);

        entity.Property(x => x.DeletedAt)
            .HasColumnName("deleted_at")
            .HasColumnType("timestamp with time zone");

        entity.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone");

        // Indexes
        entity.HasIndex(x => x.Code).IsUnique();
        entity.HasIndex(x => x.Email).IsUnique();
        entity.HasIndex(x => x.Phone).IsUnique();
        entity.HasIndex(x => x.DeletedAt);

        // Soft delete filter
        entity.HasQueryFilter(x => x.DeletedAt == null);
    }
}