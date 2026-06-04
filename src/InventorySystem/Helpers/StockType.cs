using System.Text.Json.Serialization;

namespace InventorySystem.Helpers;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StockType
{
    In = 1,
    Out = 2
}
