using FluentAssertions;
using InventorySystem.Dtos;

namespace InventorySystem.Tests.Unit;

/// <summary>
/// Tests for Category DTOs structure and validation
/// </summary>
public class CategoryDtoTests
{
    [Fact]
    public void CategoryAddRequest_WithValidData_ShouldBeValid()
    {
        // Arrange & Act
        var categoryRequest = new CategoryAddRequest
        {
            Name = "Electronics"
        };

        // Assert
        categoryRequest.Should().NotBeNull();
        categoryRequest.Name.Should().Be("Electronics");
    }

    [Fact]
    public void CategoryAddRequest_WithEmptyName_ShouldAllowCreation()
    {
        // Arrange & Act
        var categoryRequest = new CategoryAddRequest
        {
            Name = ""
        };

        // Assert - Note: Validation happens at service level
        categoryRequest.Should().NotBeNull();
        categoryRequest.Name.Should().BeEmpty();
    }

    [Fact]
    public void CategoryResponse_WithValidData_ShouldMapCorrectly()
    {
        // Arrange & Act
        var categoryResponse = new CategoryResponse
        {
            Id = 1,
            Name = "Clothing"
        };

        // Assert
        categoryResponse.Id.Should().Be(1);
        categoryResponse.Name.Should().Be("Clothing");
    }

    [Fact]
    public void CategoryUpdateRequest_ShouldUpdateName()
    {
        // Arrange & Act
        var updateRequest = new CategoryUpdateRequest
        {
            Name = "Updated Category"
        };

        // Assert
        updateRequest.Should().NotBeNull();
        updateRequest.Name.Should().Be("Updated Category");
    }
}
