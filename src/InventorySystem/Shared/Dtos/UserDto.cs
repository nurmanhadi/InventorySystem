using System.Text.Json.Serialization;
using InventorySystem.Shared.Helpers;

namespace InventorySystem.Shared.Dtos;

public class UserResponse
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("role")]
    public UserRole Role { get; set; }
}

public class UserAddRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("role")]
    public UserRole Role { get; set; }
}

public class UserUpdateRequest
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("role")]
    public UserRole? Role { get; set; }
}