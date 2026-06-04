using FluentAssertions;
using InventorySystem.Models;
using InventorySystem.Helpers;

namespace InventorySystem.Tests.Unit.Models;

public class UserModelTests
{
    [Fact]
    public void User_WithValidData_ShouldCreateSuccessfully()
    {
        // Arrange & Act
        var user = new User
        {
            Id = 1,
            Username = "testuser",
            Email = "test@example.com",
            PasswordHash = "hashed_password",
            Role = UserRole.User,
            CreatedAt = DateTime.UtcNow
        };

        // Assert
        user.Id.Should().Be(1);
        user.Username.Should().Be("testuser");
        user.Role.Should().Be(UserRole.User);
    }

    [Fact]
    public void User_CanHaveDifferentRoles()
    {
        // Arrange & Act
        var adminUser = new User
        {
            Id = 1,
            Username = "admin",
            Role = UserRole.Admin
        };

        var regularUser = new User
        {
            Id = 2,
            Username = "user",
            Role = UserRole.User
        };

        // Assert
        adminUser.Role.Should().Be(UserRole.Admin);
        regularUser.Role.Should().Be(UserRole.User);
    }

    [Fact]
    public void User_ShouldHavePasswordHash()
    {
        // Arrange & Act
        var user = new User
        {
            Id = 1,
            Username = "testuser",
            PasswordHash = "$2a$11$hashedpassword"
        };

        // Assert
        user.PasswordHash.Should().NotBeNullOrEmpty();
        user.PasswordHash.Should().StartWith("$2a$");
    }

    [Fact]
    public void User_CanBeSoftDeleted()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Username = "testuser",
            DeletedAt = null
        };

        // Act
        user.DeletedAt = DateTime.UtcNow;

        // Assert
        user.DeletedAt.Should().NotBeNull();
    }
}
