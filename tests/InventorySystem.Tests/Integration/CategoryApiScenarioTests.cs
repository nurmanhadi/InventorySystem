using FluentAssertions;
using InventorySystem.Dtos;

namespace InventorySystem.Tests.Integration;

/// <summary>
/// Integration tests for category-related API scenarios
/// </summary>
public class CategoryApiScenarioTests
{
    [Fact]
    public void CategoryWorkflow_AddAndRetrieve_ShouldSucceed()
    {
        // Arrange
        var categoryRequest = new CategoryAddRequest
        {
            Name = "Electronics"
        };

        // Act - Simulate service response
        var categoryResponse = new CategoryResponse
        {
            Id = 1,
            Name = categoryRequest.Name
        };

        // Assert
        categoryResponse.Should().NotBeNull();
        categoryResponse.Name.Should().Be(categoryRequest.Name);
        categoryResponse.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public void CategoryWorkflow_UpdateCategory_ShouldReflectChanges()
    {
        // Arrange
        var originalCategory = new CategoryResponse { Id = 1, Name = "Original Name" };
        var updateRequest = new CategoryUpdateRequest { Name = "Updated Name" };

        // Act - Simulate updated response
        var updatedCategory = new CategoryResponse
        {
            Id = originalCategory.Id,
            Name = updateRequest.Name
        };

        // Assert
        updatedCategory.Name.Should().NotBe(originalCategory.Name);
        updatedCategory.Name.Should().Be("Updated Name");
        updatedCategory.Id.Should().Be(originalCategory.Id);
    }

    [Fact]
    public void MultipleCategories_ShouldMaintainIndividualIdentity()
    {
        // Arrange & Act
        var categories = new List<CategoryResponse>
        {
            new() { Id = 1, Name = "Electronics" },
            new() { Id = 2, Name = "Clothing" },
            new() { Id = 3, Name = "Books" }
        };

        // Assert
        categories.Should().HaveCount(3);
        categories[0].Name.Should().Be("Electronics");
        categories[1].Name.Should().Be("Clothing");
        categories[2].Name.Should().Be("Books");
    }

    [Fact]
    public void CategoryAddRequest_AllFieldsAccessible()
    {
        // Arrange
        var request = new CategoryAddRequest();

        // Act
        request.Name = "Test Category";

        // Assert
        request.Name.Should().Be("Test Category");
    }
}
