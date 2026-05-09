using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Dtos;

public class CategoryAddRequest
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    public List<ProductResponse> Products { get; set; } = [];
}

public class CategoryUpdateRequest
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class CategoryResponse
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}