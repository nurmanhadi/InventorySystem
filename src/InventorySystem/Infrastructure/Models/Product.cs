using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;

namespace InventorySystem.Infrastructure.Models;

[Index(nameof(Sku), IsUnique = true)]
[Index(nameof(CreatedAt), nameof(DeletedAt))]
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

    [Column("Deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [Column("category_id")]
    public long CategoryId { get; set; }

    public NpgsqlTsVector SearchVerctor { get; set; } = default!;

    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; }

    public ICollection<Stock> Stocks { get; set; } = [];

}
