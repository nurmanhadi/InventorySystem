using System.Text.Json.Serialization;

namespace InventorySystem.Helpers;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    Admin = 1,
    Staff = 2
}