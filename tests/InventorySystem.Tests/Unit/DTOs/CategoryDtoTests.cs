using FluentAssertions;
using InventorySystem.Dtos;

namespace InventorySystem.Tests.Unit.DTOs;

public class CategoryDtoTests
{
    [Fact]
    public void CategoryRequest_WithName_ShouldCreateSuccessfully()
    {
        // Arrange & Act
        var request = new CategoryRequest { Name = "Electronics" };

        // Assert
        request.Should().NotBeNull();
        request.Name.Should().Be("Electronics");
    }

    [Fact]
    public void CategoryResponse_WithAllData_ShouldContainInfo()
    {
        // Arrange & Act
        var response = new CategoryResponse
        {
            Id = 1,
            Name = "Electronics",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Assert
        response.Id.Should().Be(1);
        response.Name.Should().Be("Electronics");
    }

    [Fact]
    public void CategoryRequest_WithEmptyString_ShouldBeAllowed()
    {
        // Arrange & Act
        var request = new CategoryRequest { Name = "" };

        // Assert
        request.Name.Should().Be("");
    }

    [Fact]
    public void CategoryRequest_CanHaveLongName()
    {
        // Arrange
        var longName = new string('A', 500);

        // Act
        var request = new CategoryRequest { Name = longName };

        // Assert
        request.Name.Should().HaveLength(500);
    }
}
