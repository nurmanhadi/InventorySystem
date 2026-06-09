using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventorySystem.Features.Products;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Features.Categories;

[Index(nameof(DeletedAt))]
[Table("categories")]
public class Category
{
    [Key]
    public long Id { get; set; }

    [Column("name", TypeName = "varchar(50)")]
    public string Name { get; set; } = string.Empty;

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    public ICollection<Product> Products { get; set; } = [];
}
