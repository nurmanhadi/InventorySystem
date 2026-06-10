namespace InventorySystem.Infrastructure.Models;

public class Supplier
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? DeletedAt { get; set; } = null;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}