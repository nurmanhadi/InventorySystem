using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventorySystem.Helpers;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Models;

[Index(nameof(ProductId), nameof(Type))]
[Table("stocks")]
public class Stock
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("product_id")]
    public long ProductId { get; set; }

    [Column("type", TypeName = "varchar(3)")]
    public StockType Type { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;
}
