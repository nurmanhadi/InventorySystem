using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Services;

public class SummaryService(DbInitiate db, ILogger<SummaryService> logger)
{
    private readonly DbInitiate db = db;
    private readonly ILogger<SummaryService> logger = logger;

    public async Task<SummaryResponse> GetSummary()
    {
        var now = DateTime.UtcNow;
        var today = now.Date;
        var tomorow = today.AddDays(1);

        var totalProduct = await db.Products.CountAsync();
        var totalCategory = await db.Categories.CountAsync();
        var totalStockInToday = await db.Stocks.Where(s => s.CreatedAt >= today && s.CreatedAt < tomorow).CountAsync();
        var totalStockOutToday = await db.Stocks.Where(s => s.CreatedAt >= today && s.CreatedAt < tomorow).CountAsync();
        var totalLowStockProduct = await db.Products.OrderBy(p => p.Stock <= 10).CountAsync();

        logger.LogInformation(
            "Summary retrieved with total product {TotalProduct}, Category {Category}, Stock in today {Sit}, Stock Out today {Sot}, Low stock product {Lsp}", totalProduct, totalCategory, totalStockInToday, totalStockOutToday, totalLowStockProduct);
        return new SummaryResponse
        {
            TotalProduct = totalProduct,
            TotalCategory = totalCategory,
            StockInToday = totalStockInToday,
            StockOutToday = totalStockOutToday,
            LowStock = totalLowStockProduct
        };
    }
}