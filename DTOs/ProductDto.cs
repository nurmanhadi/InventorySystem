using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Dtos;

public class ProductResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CategoryId { get; set; }
    public CategoryResponse? Category { get; set; } = null!;
}

public class ProductAddRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 1)]
    [JsonPropertyName("sku")]
    public string Sku { get; set; } = string.Empty;

    [Required]
    [Range(0, int.MaxValue)]
    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    [Required]
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [Required]
    [JsonPropertyName("category_id")]
    public long CategoryId { get; set; }
}

public class ProductUpdateRequest
{
    [StringLength(100, MinimumLength = 1)]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [StringLength(50, MinimumLength = 1)]
    [JsonPropertyName("sku")]
    public string? Sku { get; set; }

    [Range(0, int.MaxValue)]
    [JsonPropertyName("stock")]
    public int? Stock { get; set; }

    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    [JsonPropertyName("category_id")]
    public long? CategoryId { get; set; }
}