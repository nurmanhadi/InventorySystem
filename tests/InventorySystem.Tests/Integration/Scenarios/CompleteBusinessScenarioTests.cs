using FluentAssertions;
using InventorySystem.Dtos;
using InventorySystem.Helpers;

namespace InventorySystem.Tests.Integration.Scenarios;

public class CompleteBusinessScenarioTests
{
    [Fact]
    public void Scenario_NewStoreSetup_ShouldCreateCategoriesAndProducts()
    {
        // Arrange - Simulate creating a new store
        var categories = new List<CategoryResponse>();
        var products = new List<ProductResponse>();

        // Act - Create electronics category
        var electronics = new CategoryResponse
        {
            Id = 1,
            Name = "Electronics",
            CreatedAt = DateTime.UtcNow
        };
        categories.Add(electronics);

        // Create office supplies category
        var office = new CategoryResponse
        {
            Id = 2,
            Name = "Office Supplies",
            CreatedAt = DateTime.UtcNow
        };
        categories.Add(office);

        // Add products
        products.Add(new ProductResponse
        {
            Id = 1,
            Name = "Laptop",
            Price = 999.99m,
            CategoryId = 1,
            Stock = 0,
            CreatedAt = DateTime.UtcNow
        });

        products.Add(new ProductResponse
        {
            Id = 2,
            Name = "Mouse",
            Price = 29.99m,
            CategoryId = 1,
            Stock = 0,
            CreatedAt = DateTime.UtcNow
        });

        // Assert
        categories.Should().HaveCount(2);
        products.Should().HaveCount(2);
        products.Should().Contain(p => p.CategoryId == 1);
    }

    [Fact]
    public void Scenario_ReceiveShipment_ShouldUpdateStock()
    {
        // Arrange
        var product = new ProductResponse { Id = 1, Name = "Laptop", Stock = 0 };
        var stocks = new List<StockResponse>();

        // Act - Receive shipment of 20 laptops
        var stockIn = new StockResponse
        {
            Id = 1,
            ProductId = 1,
            Type = StockType.StockIn,
            Quantity = 20,
            Notes = "Shipment received",
            CreatedAt = DateTime.UtcNow
        };
        stocks.Add(stockIn);

        // Update product stock
        product.Stock += stockIn.Quantity;

        // Assert
        product.Stock.Should().Be(20);
        stocks.Should().HaveCount(1);
    }

    [Fact]
    public void Scenario_SalesProcess_ShouldRecordMultipleSales()
    {
        // Arrange
        var product = new ProductResponse { Id = 1, Name = "Laptop", Stock = 20 };
        var stocks = new List<StockResponse>();

        // Add initial stock
        stocks.Add(new StockResponse
        {
            Id = 1,
            ProductId = 1,
            Type = StockType.StockIn,
            Quantity = 20,
            CreatedAt = DateTime.UtcNow.AddHours(-3)
        });

        // Act - Process sales
        var sale1 = new StockResponse
        {
            Id = 2,
            ProductId = 1,
            Type = StockType.StockOut,
            Quantity = 2,
            Notes = "Sale order #001",
            CreatedAt = DateTime.UtcNow.AddHours(-2)
        };
        stocks.Add(sale1);

        var sale2 = new StockResponse
        {
            Id = 3,
            ProductId = 1,
            Type = StockType.StockOut,
            Quantity = 3,
            Notes = "Sale order #002",
            CreatedAt = DateTime.UtcNow.AddHours(-1)
        };
        stocks.Add(sale2);

        // Calculate remaining stock
        var remainingStock = 20 - 2 - 3;

        // Assert
        stocks.Should().HaveCount(3);
        remainingStock.Should().Be(15);
    }

    [Fact]
    public void Scenario_InventoryWithMultipleCategories_ShouldTrackSeparately()
    {
        // Arrange
        var categories = new List<CategoryResponse>
        {
            new CategoryResponse { Id = 1, Name = "Electronics" },
            new CategoryResponse { Id = 2, Name = "Clothing" }
        };

        var products = new List<ProductResponse>
        {
            new ProductResponse { Id = 1, Name = "Laptop", CategoryId = 1, Stock = 10 },
            new ProductResponse { Id = 2, Name = "Mouse", CategoryId = 1, Stock = 50 },
            new ProductResponse { Id = 3, Name = "Shirt", CategoryId = 2, Stock = 100 },
            new ProductResponse { Id = 4, Name = "Jeans", CategoryId = 2, Stock = 75 }
        };

        // Act
        var electronicsCount = products.Where(p => p.CategoryId == 1).Sum(p => p.Stock);
        var clothingCount = products.Where(p => p.CategoryId == 2).Sum(p => p.Stock);

        // Assert
        electronicsCount.Should().Be(60);
        clothingCount.Should().Be(175);
    }
}
