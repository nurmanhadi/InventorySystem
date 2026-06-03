using System.Text.Json.Serialization;

namespace InventorySystem.Helpers;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RolePolicy
{
    WarehouseOperations = 1,
    AdminOnly = 2,
    StaffOnly = 3
}