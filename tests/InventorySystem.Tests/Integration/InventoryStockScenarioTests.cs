using FluentAssertions;
using InventorySystem.Dtos;
using InventorySystem.Helpers;

namespace InventorySystem.Tests.Integration;

/// <summary>
/// Integration tests for inventory stock scenarios
/// </summary>
public class InventoryStockScenarioTests
{
    [Fact]
    public void StockMovement_SingleProduct_InOutTracking()
    {
        // Arrange
        var product = new ProductResponse { Id = 1, Name = "Item", Stock = 0 };
        var stockMovements = new List<StockResponse>();

        // Act - Simulate stock movements
        var stockIn = new StockResponse
        {
            Id = 1,
            ProductId = 1,
            Type = StockType.StockIn,
            Quantity = 100,
            CreatedAt = DateTime.UtcNow.AddHours(-2)
        };
        stockMovements.Add(stockIn);

        var stockOut = new StockResponse
        {
            Id = 2,
            ProductId = 1,
            Type = StockType.StockOut,
            Quantity = 30,
            CreatedAt = DateTime.UtcNow.AddHours(-1)
        };
        stockMovements.Add(stockOut);

        // Calculate current stock
        var currentStock = stockMovements
            .Where(s => s.Type == StockType.StockIn)
            .Sum(s => s.Quantity) -
            stockMovements
            .Where(s => s.Type == StockType.StockOut)
            .Sum(s => s.Quantity);

        // Assert
        stockMovements.Should().HaveCount(2);
        currentStock.Should().Be(70);
    }

    [Fact]
    public void StockMovement_MultipleProducts_SeparateTracking()
    {
        // Arrange
        var product1Movements = new List<StockResponse>
        {
            new() { ProductId = 1, Type = StockType.StockIn, Quantity = 100 },
            new() { ProductId = 1, Type = StockType.StockOut, Quantity = 25 }
        };

        var product2Movements = new List<StockResponse>
        {
            new() { ProductId = 2, Type = StockType.StockIn, Quantity = 50 },
            new() { ProductId = 2, Type = StockType.StockOut, Quantity = 10 }
        };

        // Act
        var product1Stock = product1Movements
            .Where(s => s.Type == StockType.StockIn).Sum(s => s.Quantity) -
            product1Movements.Where(s => s.Type == StockType.StockOut).Sum(s => s.Quantity);

        var product2Stock = product2Movements
            .Where(s => s.Type == StockType.StockIn).Sum(s => s.Quantity) -
            product2Movements.Where(s => s.Type == StockType.StockOut).Sum(s => s.Quantity);

        // Assert
        product1Stock.Should().Be(75);
        product2Stock.Should().Be(40);
    }

    [Fact]
    public void StockHistory_ChronologicalOrder()
    {
        // Arrange & Act
        var stockHistory = new List<StockResponse>
        {
            new() { Id = 1, CreatedAt = DateTime.UtcNow.AddHours(-3), Quantity = 100 },
            new() { Id = 2, CreatedAt = DateTime.UtcNow.AddHours(-2), Quantity = 50 },
            new() { Id = 3, CreatedAt = DateTime.UtcNow.AddHours(-1), Quantity = 30 },
            new() { Id = 4, CreatedAt = DateTime.UtcNow, Quantity = 20 }
        };

        var orderedHistory = stockHistory.OrderBy(s => s.CreatedAt).ToList();

        // Assert
        orderedHistory.Should().HaveCount(4);
        orderedHistory[0].Id.Should().Be(1);
        orderedHistory[orderedHistory.Count - 1].Id.Should().Be(4);
    }

    [Fact]
    public void LowStock_Detection()
    {
        // Arrange
        const int lowStockThreshold = 10;
        var products = new List<ProductResponse>
        {
            new() { Id = 1, Name = "Laptop", Stock = 5 },      // Low
            new() { Id = 2, Name = "Mouse", Stock = 10 },      // At threshold
            new() { Id = 3, Name = "Keyboard", Stock = 50 }    // Normal
        };

        // Act
        var lowStockItems = products.Where(p => p.Stock < lowStockThreshold).ToList();

        // Assert
        lowStockItems.Should().HaveCount(1);
        lowStockItems[0].Name.Should().Be("Laptop");
    }

    [Fact]
    public void InventorySummary_CalculateValues()
    {
        // Arrange
        var products = new List<ProductResponse>
        {
            new() { Id = 1, Name = "Laptop", Stock = 5, Price = 999.99m },
            new() { Id = 2, Name = "Mouse", Stock = 100, Price = 29.99m },
            new() { Id = 3, Name = "Keyboard", Stock = 50, Price = 89.99m }
        };

        // Act
        var totalItems = products.Sum(p => p.Stock);
        var totalValue = products.Sum(p => p.Stock * p.Price);
        var averagePrice = products.Average(p => p.Price);

        // Assert
        totalItems.Should().Be(155);
        totalValue.Should().BeApproximately(9199.35m, 0.01m);
        averagePrice.Should().BeApproximately(373.32m, 0.01m);
    }
}
