using System.Text.Json.Serialization;

namespace InventorySystem.Shared.Helpers;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SupplierActive
{
    Active = 1,
    Nonactive = 2
}