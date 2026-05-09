namespace InventorySystem.Helpers;

public class WebResponse<T>(T? data = default, string message = "Success")
{
    public T? Data { get; set; } = data;
    public string Message { get; set; } = message;
}

public class WebPaginationResponse<T>(List<T> contents, int page, int pageSize, int totalItems)
{
    public List<T> Contents { get; set; } = contents;
    public int Page { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
    public int TotalItems { get; set; } = totalItems;
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}