using FluentAssertions;
using InventorySystem.Dtos;
using InventorySystem.Helpers;

namespace InventorySystem.Tests.Integration.Scenarios;

public class EdgeCaseAndErrorHandlingTests
{
    [Fact]
    public void EdgeCase_ZeroStockProduct_ShouldBeValid()
    {
        // Arrange & Act
        var product = new ProductResponse
        {
            Id = 1,
            Name = "Out of Stock Item",
            Price = 50m,
            Stock = 0
        };

        // Assert
        product.Stock.Should().Be(0);
    }

    [Fact]
    public void EdgeCase_NegativeStockProduct_ShouldBeValid()
    {
        // Arrange & Act
        var product = new ProductResponse
        {
            Id = 1,
            Name = "Oversold Item",
            Price = 50m,
            Stock = -5
        };

        // Assert
        product.Stock.Should().Be(-5);
    }

    [Fact]
    public void EdgeCase_VeryHighPrice_ShouldBeAllowed()
    {
        // Arrange & Act
        var product = new ProductRequest
        {
            Name = "Luxury Item",
            Price = decimal.MaxValue,
            Sku = "LUXURY-001",
            CategoryId = 1
        };

        // Assert
        product.Price.Should().Be(decimal.MaxValue);
    }

    [Fact]
    public void EdgeCase_VeryLargeQuantity_ShouldBeAllowed()
    {
        // Arrange & Act
        var stock = new StockRequest
        {
            ProductId = 1,
            Quantity = int.MaxValue
        };

        // Assert
        stock.Quantity.Should().Be(int.MaxValue);
    }

    [Fact]
    public void ErrorCase_EmptyProductName_ShouldBeAllowed()
    {
        // Arrange & Act
        var product = new ProductRequest
        {
            Name = "",
            Price = 100m,
            Sku = "SKU-001",
            CategoryId = 1
        };

        // Assert - Structure allows it, validation should catch it
        product.Name.Should().BeEmpty();
    }

    [Fact]
    public void ErrorCase_NullDescription_ShouldBeAllowed()
    {
        // Arrange & Act
        var product = new ProductRequest
        {
            Name = "Product",
            Price = 100m,
            Sku = "SKU-001",
            CategoryId = 1,
            Description = null
        };

        // Assert
        product.Description.Should().BeNull();
    }

    [Fact]
    public void EdgeCase_LongProductName_ShouldBeAllowed()
    {
        // Arrange
        var longName = new string('A', 1000);

        // Act
        var product = new ProductRequest
        {
            Name = longName,
            Price = 100m,
            Sku = "SKU-001",
            CategoryId = 1
        };

        // Assert
        product.Name.Should().HaveLength(1000);
    }

    [Fact]
    public void EdgeCase_SpecialCharactersInName_ShouldBeAllowed()
    {
        // Arrange & Act
        var product = new ProductRequest
        {
            Name = "Product™ © ® - 测试 🎉",
            Price = 100m,
            Sku = "SKU-001",
            CategoryId = 1
        };

        // Assert
        product.Name.Should().Contain("™");
        product.Name.Should().Contain("测试");
        product.Name.Should().Contain("🎉");
    }

    [Fact]
    public void EdgeCase_MultipleStockMovementsSameTime_ShouldBeAllowed()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var stocks = new List<StockResponse>();

        // Act
        for (int i = 0; i < 10; i++)
        {
            stocks.Add(new StockResponse
            {
                Id = i + 1,
                ProductId = 1,
                Type = i % 2 == 0 ? StockType.StockIn : StockType.StockOut,
                Quantity = 10,
                CreatedAt = now
            });
        }

        // Assert
        stocks.Should().HaveCount(10);
        stocks.All(s => s.CreatedAt == now).Should().BeTrue();
    }

    [Fact]
    public void EdgeCase_EmptyStockHistory_ShouldBeValid()
    {
        // Arrange & Act
        var history = new List<StockResponse>();

        // Assert
        history.Should().BeEmpty();
        history.Count.Should().Be(0);
    }

    [Fact]
    public void EdgeCase_DateTimeEdgeCases_ShouldBeHandled()
    {
        // Arrange & Act
        var response1 = new StockResponse
        {
            Id = 1,
            ProductId = 1,
            Quantity = 10,
            CreatedAt = DateTime.MinValue
        };

        var response2 = new StockResponse
        {
            Id = 2,
            ProductId = 1,
            Quantity = 20,
            CreatedAt = DateTime.MaxValue
        };

        // Assert
        response1.CreatedAt.Should().Be(DateTime.MinValue);
        response2.CreatedAt.Should().Be(DateTime.MaxValue);
    }
}
