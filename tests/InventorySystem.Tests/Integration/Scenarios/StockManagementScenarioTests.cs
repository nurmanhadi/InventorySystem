using FluentAssertions;
using InventorySystem.Dtos;
using InventorySystem.Helpers;

namespace InventorySystem.Tests.Integration.Scenarios;

public class StockManagementScenarioTests
{
    [Fact]
    public void Scenario_CompleteStockCycle_ProductFromOrderToSale()
    {
        // Arrange
        var product = new ProductResponse { Id = 1, Name = "Laptop", Stock = 0 };
        var transactions = new List<StockResponse>();

        // Act - Step 1: Order received
        var orderIn = new StockResponse
        {
            Id = 1,
            ProductId = 1,
            Type = StockType.StockIn,
            Quantity = 50,
            Notes = "Supplier order #1001",
            CreatedAt = DateTime.UtcNow.AddDays(-5)
        };
        transactions.Add(orderIn);
        product.Stock += orderIn.Quantity;

        // Step 2: First sale
        var sale1 = new StockResponse
        {
            Id = 2,
            ProductId = 1,
            Type = StockType.StockOut,
            Quantity = 15,
            Notes = "Sales batch #1",
            CreatedAt = DateTime.UtcNow.AddDays(-3)
        };
        transactions.Add(sale1);
        product.Stock -= sale1.Quantity;

        // Step 3: Second sale
        var sale2 = new StockResponse
        {
            Id = 3,
            ProductId = 1,
            Type = StockType.StockOut,
            Quantity = 20,
            Notes = "Sales batch #2",
            CreatedAt = DateTime.UtcNow.AddDays(-1)
        };
        transactions.Add(sale2);
        product.Stock -= sale2.Quantity;

        // Step 4: Reorder
        var reorder = new StockResponse
        {
            Id = 4,
            ProductId = 1,
            Type = StockType.StockIn,
            Quantity = 30,
            Notes = "Supplier order #1002",
            CreatedAt = DateTime.UtcNow
        };
        transactions.Add(reorder);
        product.Stock += reorder.Quantity;

        // Assert
        product.Stock.Should().Be(45); // 50 - 15 - 20 + 30
        transactions.Should().HaveCount(4);
        transactions.Where(t => t.Type == StockType.StockIn).Sum(t => t.Quantity).Should().Be(80);
        transactions.Where(t => t.Type == StockType.StockOut).Sum(t => t.Quantity).Should().Be(35);
    }

    [Fact]
    public void Scenario_InventoryAdjustment_CorrectionOfStockCount()
    {
        // Arrange
        var product = new ProductResponse { Id = 1, Name = "Item", Stock = 100 };
        var transactions = new List<StockResponse>();

        // Act - Physical count found discrepancy
        var physicalCount = 95;
        var adjustmentNeeded = physicalCount - product.Stock; // -5

        var adjustment = new StockResponse
        {
            Id = 1,
            ProductId = 1,
            Type = StockType.StockOut,
            Quantity = Math.Abs(adjustmentNeeded),
            Notes = "Physical count adjustment",
            CreatedAt = DateTime.UtcNow
        };
        transactions.Add(adjustment);
        product.Stock = physicalCount;

        // Assert
        product.Stock.Should().Be(95);
        adjustment.Notes.Should().Contain("adjustment");
    }

    [Fact]
    public void Scenario_LowStockAlert_IdentifyRestockingNeeds()
    {
        // Arrange
        const int lowStockThreshold = 10;
        var products = new List<ProductResponse>
        {
            new ProductResponse { Id = 1, Name = "Product A", Stock = 5 },
            new ProductResponse { Id = 2, Name = "Product B", Stock = 15 },
            new ProductResponse { Id = 3, Name = "Product C", Stock = 3 },
            new ProductResponse { Id = 4, Name = "Product D", Stock = 50 }
        };

        // Act
        var lowStockItems = products.Where(p => p.Stock < lowStockThreshold).ToList();

        // Assert
        lowStockItems.Should().HaveCount(2);
        lowStockItems.Should().Contain(p => p.Id == 1);
        lowStockItems.Should().Contain(p => p.Id == 3);
    }

    [Fact]
    public void Scenario_BulkStockOut_LargeOrderProcessing()
    {
        // Arrange
        var product = new ProductResponse { Id = 1, Name = "Popular Item", Stock = 500 };
        var transactions = new List<StockResponse>();

        // Act - Large order
        var largeOrder = new StockResponse
        {
            Id = 1,
            ProductId = 1,
            Type = StockType.StockOut,
            Quantity = 250,
            Notes = "Bulk order from major customer",
            CreatedAt = DateTime.UtcNow
        };
        transactions.Add(largeOrder);
        product.Stock -= largeOrder.Quantity;

        // Assert
        product.Stock.Should().Be(250);
        transactions.Should().HaveCount(1);
    }

    [Fact]
    public void Scenario_MultiProductStockTracking_DifferentCategories()
    {
        // Arrange
        var laptops = new ProductResponse { Id = 1, Name = "Laptop", Stock = 20 };
        var mice = new ProductResponse { Id = 2, Name = "Mouse", Stock = 100 };
        var keyboards = new ProductResponse { Id = 3, Name = "Keyboard", Stock = 75 };

        var products = new List<ProductResponse> { laptops, mice, keyboards };

        // Act
        var totalStock = products.Sum(p => p.Stock);
        var avgStock = products.Average(p => p.Stock);
        var lowestStock = products.Min(p => p.Stock);

        // Assert
        totalStock.Should().Be(195);
        avgStock.Should().Be(65);
        lowestStock.Should().Be(20);
    }

    [Fact]
    public void Scenario_StockHistoryWithTimeRange_ReportGeneration()
    {
        // Arrange
        var transactions = new List<StockResponse>
        {
            new StockResponse { Id = 1, ProductId = 1, Quantity = 50, Type = StockType.StockIn, CreatedAt = DateTime.UtcNow.AddDays(-10) },
            new StockResponse { Id = 2, ProductId = 1, Quantity = 10, Type = StockType.StockOut, CreatedAt = DateTime.UtcNow.AddDays(-7) },
            new StockResponse { Id = 3, ProductId = 1, Quantity = 5, Type = StockType.StockOut, CreatedAt = DateTime.UtcNow.AddDays(-3) },
            new StockResponse { Id = 4, ProductId = 1, Quantity = 20, Type = StockType.StockIn, CreatedAt = DateTime.UtcNow.AddDays(-1) }
        };

        var startDate = DateTime.UtcNow.AddDays(-7);
        var endDate = DateTime.UtcNow;

        // Act
        var weekTransactions = transactions
            .Where(t => t.CreatedAt >= startDate && t.CreatedAt <= endDate)
            .ToList();

        // Assert
        weekTransactions.Should().HaveCount(3);
        weekTransactions.Should().NotContain(t => t.CreatedAt < startDate);
    }
}
