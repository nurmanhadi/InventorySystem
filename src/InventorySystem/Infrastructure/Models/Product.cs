using NpgsqlTypes;

namespace InventorySystem.Infrastructure.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
    public NpgsqlTsVector SearchVerctor { get; set; } = default!;
    public DateTime? DeletedAt { get; set; } = null;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Category? Category { get; set; }

}
