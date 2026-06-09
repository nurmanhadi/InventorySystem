using System.Text.Json.Serialization;
namespace InventorySystem.Features.Reports;

public class SummaryResponse
{
    [JsonPropertyName("total_product")]
    public int TotalProduct { get; set; }

    [JsonPropertyName("total_category")]
    public int TotalCategory { get; set; }

    [JsonPropertyName("stock_in_today")]
    public int StockInToday { get; set; }

    [JsonPropertyName("stock_out_today")]
    public int StockOutToday { get; set; }

    [JsonPropertyName("low_stock")]
    public int LowStock { get; set; }
}