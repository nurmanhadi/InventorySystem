using System.Text.Json.Serialization;

namespace InventorySystem.Shared.Helpers;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum HistoryStockPeriod
{
    Current = 1,
    Last7Days = 2,
    Last30Days = 3,
    Last90Days = 4,
    Yearly = 5
}