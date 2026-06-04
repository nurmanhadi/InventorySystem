using FluentAssertions;
using InventorySystem.Helpers;

namespace InventorySystem.Tests.Unit.Helpers;

public class StockTypeTests
{
    [Fact]
    public void StockType_StockIn_ShouldBeValid()
    {
        // Arrange & Act
        var stockType = StockType.StockIn;

        // Assert
        stockType.Should().Be(StockType.StockIn);
    }

    [Fact]
    public void StockType_StockOut_ShouldBeValid()
    {
        // Arrange & Act
        var stockType = StockType.StockOut;

        // Assert
        stockType.Should().Be(StockType.StockOut);
    }

    [Fact]
    public void StockType_CanCompareValues()
    {
        // Arrange
        var typeIn = StockType.StockIn;
        var typeOut = StockType.StockOut;

        // Act & Assert
        typeIn.Should().NotBe(typeOut);
        typeIn.Equals(StockType.StockIn).Should().BeTrue();
    }

    [Fact]
    public void StockType_HasCorrectNumericValues()
    {
        // Arrange & Act & Assert
        StockType.StockIn.Should().HaveValue(1);
        StockType.StockOut.Should().HaveValue(2);
    }
}
