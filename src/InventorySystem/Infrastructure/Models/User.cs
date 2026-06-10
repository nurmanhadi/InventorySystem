using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventorySystem.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Models;

[Index(nameof(Username), IsUnique = true)]
[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("username", TypeName = "varchar(100)")]
    public string Username { get; set; } = string.Empty;

    [Column("password", TypeName = "varchar(255)")]
    public string Password { get; set; } = string.Empty;

    [Column("role", TypeName = "varchar(10)")]
    public UserRole Role { get; set; }
}