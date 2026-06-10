using System.Text.Json.Serialization;

namespace InventorySystem.Shared.Dtos;

public class AuthLoginRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}