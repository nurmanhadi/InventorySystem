using System.Text.Json.Serialization;

namespace InventorySystem.Helpers;

public class WebResponse<T>(T? data = default, string message = "Success")
{
    [JsonPropertyName("data")]
    public T? Data { get; set; } = data;
    [JsonPropertyName("message")]
    public string Message { get; set; } = message;
}

public class WebPaginationResponse<T>(List<T> contents, int page, int pageSize, int totalItems)
{
    [JsonPropertyName("contents")]
    public List<T> Contents { get; set; } = contents;
    [JsonPropertyName("current_page")]
    public int Page { get; set; } = page;
    [JsonPropertyName("page_size")]
    public int PageSize { get; set; } = pageSize;
    [JsonPropertyName("total_items")]
    public int TotalItems { get; set; } = totalItems;
    [JsonPropertyName("total_pages")]
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}