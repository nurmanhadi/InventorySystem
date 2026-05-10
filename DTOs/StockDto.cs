using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using InventorySystem.Helpers;

namespace InventorySystem.Dtos;

public class StockResponse
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public StockType Type { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class StockWithProductMinimalResponse
{
    public long Id { get; set; }
    public StockType Type { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public ProductMinimalResponse Product { get; set; } = new ProductMinimalResponse();
}
public class StockRequest
{
    [Required]
    [JsonPropertyName("product_id")]
    public long ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}