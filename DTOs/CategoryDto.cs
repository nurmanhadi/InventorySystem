using System.Text.Json.Serialization;

namespace InventorySystem.Dtos;

public class CategoryAddRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class CategoryUpdateRequest
{
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