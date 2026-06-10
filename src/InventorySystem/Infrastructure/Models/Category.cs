namespace InventorySystem.Infrastructure.Models;

public class Category
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? DeletedAt { get; set; } = null;

    public ICollection<Product> Products { get; set; } = [];
}
