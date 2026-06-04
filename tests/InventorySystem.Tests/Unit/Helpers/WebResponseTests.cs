using FluentAssertions;
using InventorySystem.Helpers;
using InventorySystem.Dtos;

namespace InventorySystem.Tests.Unit.Helpers;

public class WebResponseTests
{
    [Fact]
    public void WebResponse_SuccessResponse_ShouldHaveCorrectStructure()
    {
        // Arrange & Act
        var response = new WebResponse<string>
        {
            Success = true,
            Message = "Operation successful",
            Data = "test data"
        };

        // Assert
        response.Success.Should().BeTrue();
        response.Message.Should().Be("Operation successful");
        response.Data.Should().Be("test data");
    }

    [Fact]
    public void WebResponse_ErrorResponse_ShouldHandleNull()
    {
        // Arrange & Act
        var response = new WebResponse<string>
        {
            Success = false,
            Message = "Operation failed",
            Data = null
        };

        // Assert
        response.Success.Should().BeFalse();
        response.Message.Should().Be("Operation failed");
        response.Data.Should().BeNull();
    }

    [Fact]
    public void WebResponse_CanCreateGenericResponse()
    {
        // Arrange
        var dataList = new List<string> { "item1", "item2", "item3" };

        // Act
        var response = new WebResponse<List<string>>
        {
            Success = true,
            Message = "Data retrieved",
            Data = dataList
        };

        // Assert
        response.Data.Should().HaveCount(3);
        response.Data.Should().Contain("item1");
    }
}
