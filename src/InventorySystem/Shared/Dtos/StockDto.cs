using System.Text.Json.Serialization;
using InventorySystem.Shared.Helpers;

namespace InventorySystem.Shared.Dtos;

public class StockResponse
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("product_id")]
    public long ProductId { get; set; }

    [JsonPropertyName("type")]
    public StockType Type { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}
public class StockWithProductMinimalResponse
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("type")]
    public StockType Type { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("product")]
    public ProductMinimalResponse Product { get; set; } = new ProductMinimalResponse();
}
public class StockRequest
{
    [JsonPropertyName("product_id")]
    public long ProductId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}