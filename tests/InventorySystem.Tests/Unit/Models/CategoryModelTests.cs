using FluentAssertions;
using InventorySystem.Models;

namespace InventorySystem.Tests.Unit.Models;

public class CategoryModelTests
{
    [Fact]
    public void Category_WithValidData_ShouldCreateSuccessfully()
    {
        // Arrange & Act
        var category = new Category
        {
            Id = 1,
            Name = "Electronics",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedAt = null
        };

        // Assert
        category.Id.Should().Be(1);
        category.Name.Should().Be("Electronics");
        category.DeletedAt.Should().BeNull();
    }

    [Fact]
    public void Category_ShouldSupportSoftDelete()
    {
        // Arrange
        var category = new Category
        {
            Id = 1,
            Name = "Electronics",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedAt = null
        };

        // Act
        var deletedAt = DateTime.UtcNow;
        category.DeletedAt = deletedAt;

        // Assert
        category.DeletedAt.Should().NotBeNull();
        category.DeletedAt.Should().BeCloseTo(deletedAt, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Category_CanHaveMultipleProducts()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Electronics" };
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", CategoryId = 1 },
            new Product { Id = 2, Name = "Mouse", CategoryId = 1 }
        };

        // Act
        category.Products = products;

        // Assert
        category.Products.Should().HaveCount(2);
    }
}
