using InventorySystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventorySystem.Infrastructure.Schemas;

public class UserSchema : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("users");
        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id)
        .HasColumnName("id")
        .ValueGeneratedOnAdd();

        entity.Property(x => x.Username)
        .HasColumnName("username")
        .HasColumnType("varchar(100)")
        .IsRequired();

        entity.Property(x => x.Password)
        .HasColumnName("password")
        .HasColumnType("varchar(100)")
        .IsRequired();

        entity.Property(x => x.Role)
        .HasColumnName("role")
        .HasColumnType("varchar(10)")
        .HasConversion<string>()
        .IsRequired();

        entity.HasIndex(x => x.Username).IsUnique();
    }
}