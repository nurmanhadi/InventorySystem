using FluentAssertions;
using InventorySystem.Dtos;
using InventorySystem.Helpers;

namespace InventorySystem.Tests.Unit.DTOs;

public class UserDtoTests
{
    [Fact]
    public void UserRequest_WithValidData_ShouldCreateSuccessfully()
    {
        // Arrange & Act
        var request = new UserRequest
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = "SecurePass123!",
            Role = UserRole.User
        };

        // Assert
        request.Should().NotBeNull();
        request.Username.Should().Be("testuser");
        request.Email.Should().Be("test@example.com");
    }

    [Fact]
    public void UserResponse_WithAllData_ShouldContainInfo()
    {
        // Arrange & Act
        var response = new UserResponse
        {
            Id = 1,
            Username = "testuser",
            Email = "test@example.com",
            Role = UserRole.User,
            CreatedAt = DateTime.UtcNow
        };

        // Assert
        response.Id.Should().Be(1);
        response.Username.Should().Be("testuser");
        response.Role.Should().Be(UserRole.User);
    }

    [Fact]
    public void UserRequest_CanHaveDifferentRoles()
    {
        // Arrange & Act
        var adminRequest = new UserRequest
        {
            Username = "admin",
            Role = UserRole.Admin,
            Password = "Pass123!"
        };

        var userRequest = new UserRequest
        {
            Username = "user",
            Role = UserRole.User,
            Password = "Pass123!"
        };

        // Assert
        adminRequest.Role.Should().Be(UserRole.Admin);
        userRequest.Role.Should().Be(UserRole.User);
    }

    [Fact]
    public void UserResponse_WithoutSensitiveData()
    {
        // Arrange & Act
        var response = new UserResponse
        {
            Id = 1,
            Username = "testuser",
            Email = "test@example.com",
            Role = UserRole.User
        };

        // Assert - Should not contain password
        response.Should().NotHaveProperty("Password");
    }
}
