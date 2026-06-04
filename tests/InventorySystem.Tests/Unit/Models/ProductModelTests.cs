using FluentAssertions;
using InventorySystem.Models;

namespace InventorySystem.Tests.Unit.Models;

public class ProductModelTests
{
    [Fact]
    public void Product_WithValidData_ShouldCreateSuccessfully()
    {
        // Arrange & Act
        var product = new Product
        {
            Id = 1,
            Name = "Laptop",
            Price = 999.99m,
            Sku = "LAP-001",
            CategoryId = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Assert
        product.Id.Should().Be(1);
        product.Name.Should().Be("Laptop");
        product.Price.Should().Be(999.99m);
    }

    [Fact]
    public void Product_ShouldSupportSoftDelete()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Laptop",
            Price = 999.99m,
            DeletedAt = null
        };

        // Act
        product.DeletedAt = DateTime.UtcNow;

        // Assert
        product.DeletedAt.Should().NotBeNull();
    }

    [Fact]
    public void Product_CanHaveDescription()
    {
        // Arrange & Act
        var product = new Product
        {
            Id = 1,
            Name = "Laptop",
            Price = 999.99m,
            Description = "High-performance laptop"
        };

        // Assert
        product.Description.Should().Be("High-performance laptop");
    }

    [Fact]
    public void Product_CanHaveMultipleStockMovements()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Laptop" };
        var stocks = new List<Stock>
        {
            new Stock { Id = 1, ProductId = 1, Quantity = 50 },
            new Stock { Id = 2, ProductId = 1, Quantity = -10 }
        };

        // Act
        product.Stocks = stocks;

        // Assert
        product.Stocks.Should().HaveCount(2);
    }
}
