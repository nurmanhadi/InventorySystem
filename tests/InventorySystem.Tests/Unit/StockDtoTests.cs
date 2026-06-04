using FluentAssertions;
using InventorySystem.Dtos;
using InventorySystem.Helpers;

namespace InventorySystem.Tests.Unit;

/// <summary>
/// Tests for Stock DTOs structure and validation
/// </summary>
public class StockDtoTests
{
    [Fact]
    public void StockRequest_WithValidData_ShouldMapCorrectly()
    {
        // Arrange & Act
        var stockRequest = new StockRequest
        {
            ProductId = 1,
            Quantity = 50
        };

        // Assert
        stockRequest.Should().NotBeNull();
        stockRequest.ProductId.Should().Be(1);
        stockRequest.Quantity.Should().Be(50);
    }

    [Fact]
    public void StockResponse_WithValidData_ShouldContainAllInfo()
    {
        // Arrange & Act
        var stockResponse = new StockResponse
        {
            Id = 1,
            ProductId = 1,
            Type = StockType.StockIn,
            Quantity = 100,
            CreatedAt = DateTime.UtcNow
        };

        // Assert
        stockResponse.Should().NotBeNull();
        stockResponse.Id.Should().Be(1);
        stockResponse.Type.Should().Be(StockType.StockIn);
        stockResponse.Quantity.Should().Be(100);
    }

    [Fact]
    public void StockRequest_CanHandleZeroQuantity()
    {
        // Arrange & Act
        var stockRequest = new StockRequest
        {
            ProductId = 1,
            Quantity = 0
        };

        // Assert
        stockRequest.Should().NotBeNull();
        stockRequest.Quantity.Should().Be(0);
    }

    [Fact]
    public void MultipleStockResponses_ShouldDifferentiateTypes()
    {
        // Arrange & Act
        var stockIn = new StockResponse
        {
            Type = StockType.StockIn,
            Quantity = 100
        };

        var stockOut = new StockResponse
        {
            Type = StockType.StockOut,
            Quantity = 30
        };

        // Assert
        stockIn.Type.Should().Be(StockType.StockIn);
        stockOut.Type.Should().Be(StockType.StockOut);
        (stockIn.Quantity - stockOut.Quantity).Should().Be(70);
    }

    [Fact]
    public void StockWithProductMinimalResponse_ShouldIncludeProduct()
    {
        // Arrange & Act
        var productMinimal = new ProductMinimalResponse { Id = 1, Name = "Laptop" };

        var stockWithProduct = new StockWithProductMinimalResponse
        {
            Id = 1,
            Type = StockType.StockIn,
            Quantity = 50,
            Product = productMinimal
        };

        // Assert
        stockWithProduct.Should().NotBeNull();
        stockWithProduct.Product.Should().NotBeNull();
        stockWithProduct.Product.Name.Should().Be("Laptop");
    }
}
