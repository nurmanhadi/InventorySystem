using FluentAssertions;
using InventorySystem.Dtos;

namespace InventorySystem.Tests.Unit;

/// <summary>
/// Tests for Product DTOs structure and validation
/// </summary>
public class ProductDtoTests
{
    [Fact]
    public void ProductResponse_WithValidData_ShouldMapCorrectly()
    {
        // Arrange & Act
        var productResponse = new ProductResponse
        {
            Id = 1,
            Name = "Laptop",
            Sku = "LAP-001",
            Stock = 50,
            Price = 999.99m
        };

        // Assert
        productResponse.Should().NotBeNull();
        productResponse.Id.Should().Be(1);
        productResponse.Name.Should().Be("Laptop");
        productResponse.Sku.Should().Be("LAP-001");
        productResponse.Stock.Should().Be(50);
        productResponse.Price.Should().Be(999.99m);
    }

    [Fact]
    public void ProductWithCategoryResponse_ShouldIncludeCategory()
    {
        // Arrange & Act
        var categoryResponse = new CategoryResponse
        {
            Id = 1,
            Name = "Electronics"
        };

        var productResponse = new ProductWithCategoryResponse
        {
            Id = 1,
            Name = "Laptop",
            Sku = "LAP-001",
            Stock = 50,
            Price = 999.99m,
            Category = categoryResponse
        };

        // Assert
        productResponse.Should().NotBeNull();
        productResponse.Category.Should().NotBeNull();
        productResponse.Category.Name.Should().Be("Electronics");
    }

    [Fact]
    public void ProductMinimalResponse_ShouldContainOnlyBasicInfo()
    {
        // Arrange & Act
        var minimalResponse = new ProductMinimalResponse
        {
            Id = 1,
            Name = "Laptop"
        };

        // Assert
        minimalResponse.Should().NotBeNull();
        minimalResponse.Id.Should().Be(1);
        minimalResponse.Name.Should().Be("Laptop");
    }

    [Fact]
    public void MultipleProductResponses_ShouldMaintainIndividualValues()
    {
        // Arrange
        var product1 = new ProductResponse { Id = 1, Name = "Laptop", Price = 999.99m };
        var product2 = new ProductResponse { Id = 2, Name = "Mouse", Price = 29.99m };
        var product3 = new ProductResponse { Id = 3, Name = "Monitor", Price = 299.99m };

        // Act
        var products = new[] { product1, product2, product3 };

        // Assert
        products.Should().HaveCount(3);
        products[0].Name.Should().Be("Laptop");
        products[1].Name.Should().Be("Mouse");
        products[2].Price.Should().Be(299.99m);
    }
}
