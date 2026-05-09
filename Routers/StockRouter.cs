using InventorySystem.Dtos;
using InventorySystem.Helpers;
using InventorySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Routers;

public static class StockRouter
{
    public static void MapStockRoutes(this WebApplication app)
    {
        var stock = app.MapGroup("/stocks").WithTags("Stock");

        // stock in
        stock.MapPost("/in", async (
            [FromServices] StockService stockService,
            [FromBody] StockRequest request
            ) =>
        {
            await stockService.StockIn(request);
            return Results.Ok(new WebResponse<string>("Stock in successful"));
        });

        // stock out
        stock.MapPost("/out", async (
            [FromServices] StockService stockService,
            [FromBody] StockRequest request
            ) =>
        {
            await stockService.StockOut(request);
            return Results.Ok(new WebResponse<string>("Stock out successful"));
        });

        // stock history
        stock.MapGet("/history", async (
            [FromServices] StockService stockService,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] HistoryStockPeriod period = HistoryStockPeriod.CURRENT,
            [FromQuery] long? productId = null,
            [FromQuery] StockType? type = null
            ) =>
        {
            var response = await stockService.GetStockHistory(page, size, period, productId, type);
            return Results.Ok(response);
        });
    }
}