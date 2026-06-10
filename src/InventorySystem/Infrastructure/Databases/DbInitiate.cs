namespace InventorySystem.Infrastructure.Databases;

using InventorySystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;


public class DbInitiate(DbContextOptions<DbInitiate> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbInitiate).Assembly);
    }
}
