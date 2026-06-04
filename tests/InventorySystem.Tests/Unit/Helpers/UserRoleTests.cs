using FluentAssertions;
using InventorySystem.Helpers;

namespace InventorySystem.Tests.Unit.Helpers;

public class UserRoleTests
{
    [Fact]
    public void UserRole_Admin_ShouldExist()
    {
        // Arrange & Act
        var role = UserRole.Admin;

        // Assert
        role.Should().Be(UserRole.Admin);
    }

    [Fact]
    public void UserRole_Manager_ShouldExist()
    {
        // Arrange & Act
        var role = UserRole.Manager;

        // Assert
        role.Should().Be(UserRole.Manager);
    }

    [Fact]
    public void UserRole_User_ShouldExist()
    {
        // Arrange & Act
        var role = UserRole.User;

        // Assert
        role.Should().Be(UserRole.User);
    }

    [Fact]
    public void UserRole_AllRolesShouldBeUnique()
    {
        // Arrange & Act
        var admin = UserRole.Admin;
        var manager = UserRole.Manager;
        var user = UserRole.User;

        // Assert
        admin.Should().NotBe(manager);
        manager.Should().NotBe(user);
        admin.Should().NotBe(user);
    }

    [Fact]
    public void UserRole_ShouldHaveCorrectValues()
    {
        // Arrange & Act & Assert
        ((int)UserRole.Admin).Should().Be(1);
        ((int)UserRole.Manager).Should().Be(2);
        ((int)UserRole.User).Should().Be(3);
    }
}
