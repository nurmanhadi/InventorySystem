using FluentAssertions;
using InventorySystem.Dtos;

namespace InventorySystem.Tests.Unit.DTOs;

public class ProductDtoTests
{
    [Fact]
    public void ProductRequest_WithValidData_ShouldCreateSuccessfully()
    {
        // Arrange & Act
        var request = new ProductRequest
        {
            Name = "Laptop",
            Price = 999.99m,
            Sku = "LAP-001",
            CategoryId = 1,
            Description = "High-performance laptop"
        };

        // Assert
        request.Should().NotBeNull();
        request.Name.Should().Be("Laptop");
        request.Price.Should().Be(999.99m);
        request.Sku.Should().Be("LAP-001");
    }

    [Fact]
    public void ProductResponse_WithAllData_ShouldContainCompleteInfo()
    {
        // Arrange & Act
        var response = new ProductResponse
        {
            Id = 1,
            Name = "Laptop",
            Price = 999.99m,
            Sku = "LAP-001",
            CategoryId = 1,
            Stock = 50,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Assert
        response.Id.Should().Be(1);
        response.Name.Should().Be("Laptop");
        response.Stock.Should().Be(50);
    }

    [Fact]
    public void ProductRequest_WithZeroPrice_ShouldBeAllowed()
    {
        // Arrange & Act
        var request = new ProductRequest
        {
            Name = "Free Item",
            Price = 0m,
            Sku = "FREE-001",
            CategoryId = 1
        };

        // Assert
        request.Price.Should().Be(0m);
    }

    [Fact]
    public void ProductResponse_CanHaveNegativeStock()
    {
        // Arrange & Act
        var response = new ProductResponse
        {
            Id = 1,
            Name = "Product",
            Stock = -5,
            Price = 100m
        };

        // Assert
        response.Stock.Should().Be(-5);
    }
}
