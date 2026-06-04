using FluentAssertions;
using InventorySystem.Models;
using InventorySystem.Helpers;

namespace InventorySystem.Tests.Unit.Models;

public class StockModelTests
{
    [Fact]
    public void Stock_WithValidData_ShouldCreateSuccessfully()
    {
        // Arrange & Act
        var stock = new Stock
        {
            Id = 1,
            ProductId = 1,
            Quantity = 100,
            Type = StockType.StockIn,
            CreatedAt = DateTime.UtcNow
        };

        // Assert
        stock.Id.Should().Be(1);
        stock.ProductId.Should().Be(1);
        stock.Quantity.Should().Be(100);
        stock.Type.Should().Be(StockType.StockIn);
    }

    [Fact]
    public void Stock_CanHaveNegativeQuantity()
    {
        // Arrange & Act
        var stock = new Stock
        {
            Id = 1,
            ProductId = 1,
            Quantity = -50,
            Type = StockType.StockOut
        };

        // Assert
        stock.Quantity.Should().Be(-50);
    }

    [Fact]
    public void Stock_CanHaveNotes()
    {
        // Arrange & Act
        var stock = new Stock
        {
            Id = 1,
            ProductId = 1,
            Quantity = 50,
            Type = StockType.StockIn,
            Notes = "Initial inventory"
        };

        // Assert
        stock.Notes.Should().Be("Initial inventory");
    }

    [Fact]
    public void Stock_CanHaveEmptyNotes()
    {
        // Arrange & Act
        var stock = new Stock
        {
            Id = 1,
            ProductId = 1,
            Quantity = 50,
            Type = StockType.StockIn,
            Notes = null
        };

        // Assert
        stock.Notes.Should().BeNull();
    }

    [Fact]
    public void Stock_ShouldBelongToProduct()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Laptop" };
        var stock = new Stock
        {
            Id = 1,
            ProductId = 1,
            Quantity = 50,
            Product = product
        };

        // Act & Assert
        stock.Product.Should().Be(product);
        stock.ProductId.Should().Be(product.Id);
    }
}
