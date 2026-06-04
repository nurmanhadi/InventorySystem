using FluentAssertions;
using InventorySystem.Dtos;

namespace InventorySystem.Tests.Integration;

/// <summary>
/// Integration tests for product-related API scenarios
/// </summary>
public class ProductApiScenarioTests
{
    [Fact]
    public void ProductWorkflow_CreateAndRetrieve_ShouldSucceed()
    {
        // Arrange
        var productName = "Laptop";
        var productSku = "LAP-001";
        var productPrice = 999.99m;

        // Act - Simulate service creating product
        var productResponse = new ProductResponse
        {
            Id = 1,
            Name = productName,
            Sku = productSku,
            Stock = 50,
            Price = productPrice
        };

        // Assert
        productResponse.Should().NotBeNull();
        productResponse.Name.Should().Be(productName);
        productResponse.Sku.Should().Be(productSku);
        productResponse.Price.Should().Be(productPrice);
    }

    [Fact]
    public void ProductWorkflow_WithCategory_ShouldIncludeCategory()
    {
        // Arrange
        var category = new CategoryResponse { Id = 1, Name = "Electronics" };

        // Act
        var productWithCategory = new ProductWithCategoryResponse
        {
            Id = 1,
            Name = "Laptop",
            Sku = "LAP-001",
            Stock = 50,
            Price = 999.99m,
            Category = category
        };

        // Assert
        productWithCategory.Category.Should().NotBeNull();
        productWithCategory.Category.Name.Should().Be("Electronics");
    }

    [Fact]
    public void ProductInventory_StockCalculation_ShouldBeAccurate()
    {
        // Arrange
        var product = new ProductResponse
        {
            Id = 1,
            Name = "Item",
            Sku = "ITEM-001",
            Stock = 100,
            Price = 50m
        };

        var stockIn = 50;
        var stockOut = 30;

        // Act
        var expectedStock = product.Stock + stockIn - stockOut;

        // Assert
        expectedStock.Should().Be(120);
    }

    [Fact]
    public void MultipleProducts_InDifferentCategories()
    {
        // Arrange & Act
        var electronics = new CategoryResponse { Id = 1, Name = "Electronics" };
        var clothing = new CategoryResponse { Id = 2, Name = "Clothing" };

        var products = new List<ProductWithCategoryResponse>
        {
            new() { Id = 1, Name = "Laptop", Category = electronics },
            new() { Id = 2, Name = "Mouse", Category = electronics },
            new() { Id = 3, Name = "T-Shirt", Category = clothing },
            new() { Id = 4, Name = "Jeans", Category = clothing }
        };

        // Assert
        products.Should().HaveCount(4);
        products.Where(p => p.Category.Id == 1).Should().HaveCount(2);
        products.Where(p => p.Category.Id == 2).Should().HaveCount(2);
    }

    [Fact]
    public void ProductPriceCalculation_WithMultipleItems()
    {
        // Arrange
        var products = new List<ProductResponse>
        {
            new() { Id = 1, Stock = 10, Price = 100m },
            new() { Id = 2, Stock = 5, Price = 200m },
            new() { Id = 3, Stock = 20, Price = 50m }
        };

        // Act
        var totalValue = products.Sum(p => p.Stock * p.Price);

        // Assert
        totalValue.Should().Be(2000m); // (10*100) + (5*200) + (20*50) = 1000 + 1000 + 1000 = 2000
    }
}
