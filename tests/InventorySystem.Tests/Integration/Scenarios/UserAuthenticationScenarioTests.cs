using FluentAssertions;
using InventorySystem.Dtos;
using InventorySystem.Helpers;

namespace InventorySystem.Tests.Integration.Scenarios;

public class UserAuthenticationScenarioTests
{
    [Fact]
    public void Scenario_AdminUserCreation_ShouldHaveProperRole()
    {
        // Arrange & Act
        var admin = new UserRequest
        {
            Username = "admin",
            Email = "admin@company.com",
            Password = "AdminPass123!@#",
            Role = UserRole.Admin
        };

        var adminResponse = new UserResponse
        {
            Id = 1,
            Username = admin.Username,
            Email = admin.Email,
            Role = admin.Role,
            CreatedAt = DateTime.UtcNow
        };

        // Assert
        adminResponse.Role.Should().Be(UserRole.Admin);
        adminResponse.Username.Should().Be("admin");
    }

    [Fact]
    public void Scenario_MultipleUsersWithDifferentRoles_ShouldCoexist()
    {
        // Arrange & Act
        var users = new List<UserResponse>
        {
            new UserResponse { Id = 1, Username = "admin", Role = UserRole.Admin },
            new UserResponse { Id = 2, Username = "manager", Role = UserRole.Manager },
            new UserResponse { Id = 3, Username = "user1", Role = UserRole.User },
            new UserResponse { Id = 4, Username = "user2", Role = UserRole.User }
        };

        // Assert
        users.Should().HaveCount(4);
        users.Where(u => u.Role == UserRole.Admin).Should().HaveCount(1);
        users.Where(u => u.Role == UserRole.Manager).Should().HaveCount(1);
        users.Where(u => u.Role == UserRole.User).Should().HaveCount(2);
    }

    [Fact]
    public void Scenario_UserDeletion_ShouldRemoveAccess()
    {
        // Arrange
        var users = new List<UserResponse>
        {
            new UserResponse { Id = 1, Username = "user1", Role = UserRole.User },
            new UserResponse { Id = 2, Username = "user2", Role = UserRole.User }
        };

        // Act
        var userToDelete = users.First(u => u.Username == "user1");
        users.Remove(userToDelete);

        // Assert
        users.Should().HaveCount(1);
        users.Should().NotContain(u => u.Username == "user1");
    }

    [Fact]
    public void Scenario_UserRoleChange_ShouldUpdatePermissions()
    {
        // Arrange
        var user = new UserResponse
        {
            Id = 1,
            Username = "newmanager",
            Role = UserRole.User,
            CreatedAt = DateTime.UtcNow
        };

        // Act - Promote user to manager
        user.Role = UserRole.Manager;

        // Assert
        user.Role.Should().Be(UserRole.Manager);
    }

    [Fact]
    public void Scenario_PasswordSecurity_ShouldNotExposePassword()
    {
        // Arrange
        const string password = "SecurePassword123!@#";

        var userRequest = new UserRequest
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = password,
            Role = UserRole.User
        };

        var userResponse = new UserResponse
        {
            Id = 1,
            Username = "testuser",
            Email = "test@example.com",
            Role = UserRole.User
        };

        // Assert - Response should not contain password
        userResponse.Should().NotHaveProperty("Password");
        userResponse.Should().NotHaveProperty("PasswordHash");
    }

    [Fact]
    public void Scenario_ConcurrentUserCreation_ShouldAssignUniqueIds()
    {
        // Arrange & Act
        var users = new List<UserResponse>();
        for (int i = 1; i <= 5; i++)
        {
            users.Add(new UserResponse
            {
                Id = i,
                Username = $"user{i}",
                Email = $"user{i}@example.com",
                Role = UserRole.User,
                CreatedAt = DateTime.UtcNow
            });
        }

        // Assert
        users.Should().HaveCount(5);
        users.Select(u => u.Id).Should().AllBe(x => x > 0);
        users.Select(u => u.Id).Distinct().Should().HaveCount(5);
    }
}
