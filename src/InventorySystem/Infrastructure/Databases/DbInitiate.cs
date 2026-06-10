namespace InventorySystem.Infrastructure.Databases;

using InventorySystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;


public class DbInitiate(DbContextOptions<DbInitiate> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Stock> Stocks => Set<Stock>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
        .HasGeneratedTsVectorColumn(
            p => p.SearchVerctor,
            "english",
            p => new { p.Name, p.Sku }
        )
        .HasIndex(p => p.SearchVerctor)
        .HasMethod("GIN");

        modelBuilder.Entity<Stock>()
            .Property(x => x.Type)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .Property(x => x.Role)
            .HasConversion<string>();
    }
}
