using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Models;

[Index(nameof(Sku), IsUnique = true)]
[Table("products")]
public class Product
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("name", TypeName = "varchar(100)")]
    public string Name { get; set; } = string.Empty;

    [Column("sku", TypeName = "varchar(50)")]
    public string Sku { get; set; } = string.Empty;

    [Column("stock")]
    public int Stock { get; set; }

    [Column("price", TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("category_id")]
    public long CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;

    public ICollection<Stock> Stocks { get; set; } = [];

}
