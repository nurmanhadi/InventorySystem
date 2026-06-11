using System.Text.Json.Serialization;

namespace InventorySystem.Shared.Helpers;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SupplierOrderBy
{
    Code = 1,
    Name = 2,
    Email = 3,
    Phone = 4
}