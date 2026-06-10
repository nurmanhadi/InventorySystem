using NpgsqlTypes;

namespace InventorySystem.Infrastructure.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; } = null;
    public long CategoryId { get; set; }
    public NpgsqlTsVector SearchVerctor { get; set; } = default!;
    public Category? Category { get; set; }

}
