using FluentValidation;
using InventorySystem.Dtos;
using InventorySystem.Exceptions;
using InventorySystem.Helpers;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Services;

public class StockService(DbInitiate db, IValidator<StockRequest> validator)
{
    private readonly DbInitiate db = db;
    private readonly IValidator<StockRequest> validator = validator;

    // stock in
    public async Task StockIn(StockRequest request)
    {
        await StockValidation(request);
        await CheckProductExistsAsync(request.ProductId);
        await using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            var stock = new Stock
            {
                ProductId = request.ProductId,
                Type = StockType.IN,
                Quantity = request.Quantity,
                CreatedAt = DateTime.UtcNow
            };

            db.Stocks.Add(stock);
            await UpdateStockProductAsync(request.ProductId, request.Quantity, StockType.IN);
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

    }
    // stock out
    public async Task StockOut(StockRequest request)
    {
        await StockValidation(request);
        await CheckProductExistsAsync(request.ProductId);
        await using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            var stock = new Stock
            {
                ProductId = request.ProductId,
                Type = StockType.OUT,
                Quantity = request.Quantity,
                CreatedAt = DateTime.UtcNow
            };

            db.Stocks.Add(stock);
            await UpdateStockProductAsync(request.ProductId, request.Quantity, StockType.OUT);
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    // get stock history
    public async Task<WebPaginationResponse<StockWithProductMinimalResponse>> GetStockHistory(int page, int size, HistoryStockPeriod period, long? productId, StockType? type = null)
    {
        var query = db.Stocks
        .AsNoTracking()
        .AsQueryable();
        if (productId.HasValue)
        {
            query = query.Where(s => s.ProductId == productId.Value);
        }

        if (type.HasValue)
        {
            query = query.Where(s => s.Type == type.Value);
        }

        var now = DateTime.UtcNow;
        var today = now.Date;
        var tomorrow = today.AddDays(1);
        query = period switch
        {
            HistoryStockPeriod.CURRENT => query.Where(s => s.CreatedAt >= today && s.CreatedAt < tomorrow),
            HistoryStockPeriod.LAST_7_DAYS => query.Where(s => s.CreatedAt >= now.AddDays(-7)),
            HistoryStockPeriod.LAST_30_DAYS => query.Where(s => s.CreatedAt >= now.AddDays(-30)),
            HistoryStockPeriod.LAST_90_DAYS => query.Where(s => s.CreatedAt >= now.AddDays(-90)),
            HistoryStockPeriod.YEARLY => query.Where(s => s.CreatedAt >= now.AddYears(-1)),
            _ => query
        };

        var totalItems = await query.CountAsync();
        var stocks = await query
            .OrderByDescending(s => s.CreatedAt)
            .Skip((page - 1) * size)
            .Take(size)
            .Select(s => new StockWithProductMinimalResponse
            {
                Id = s.Id,
                Type = s.Type,
                Quantity = s.Quantity,
                CreatedAt = s.CreatedAt,
                Product = new ProductMinimalResponse
                {
                    Id = s.Product!.Id,
                    Name = s.Product!.Name,
                    Sku = s.Product!.Sku
                }
            })
            .ToListAsync();
        return new WebPaginationResponse<StockWithProductMinimalResponse>(stocks, page, size, totalItems);
    }

    // check product exist
    private async Task CheckProductExistsAsync(long productId)
    {
        var product = await db.Products.CountAsync(p => p.Id == productId);
        if (product == 0)
        {
            throw new NotFoundException($"Product with id {productId} not found");
        }
    }

    // update stock product
    private async Task UpdateStockProductAsync(long productId, int quantity, StockType type)
    {
        var product = await db.Products.FindAsync(productId) ?? throw new NotFoundException($"Product with id {productId} not found");
        switch (type)
        {
            case StockType.IN:
                product.Stock += quantity;
                break;
            case StockType.OUT:
                if (product.Stock < quantity)
                {
                    throw new BadRequestException($"Not enough stock for product with id {productId}");
                }
                product.Stock -= quantity;
                break;
        }
    }

    // stock validation
    public async Task StockValidation(StockRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
        }
    }

}