using System.Text.Json.Serialization;

namespace InventorySystem.Shared.Helpers;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderBy
{
    Ascending = 1,
    Descending = 2
}