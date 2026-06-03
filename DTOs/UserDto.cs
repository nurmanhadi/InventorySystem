using System.Text.Json.Serialization;
using InventorySystem.Helpers;

namespace InventorySystem.Dtos;

public class UserResponse
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("role")]
    public UserRole Role { get; set; }
}