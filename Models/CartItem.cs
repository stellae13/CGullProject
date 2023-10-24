using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject;

[Owned]
[PrimaryKey(nameof(CartId),nameof(InventoryId))]
public class CartItem {

    [Required]
    [ForeignKey("Cart")]
    [Column(TypeName = "uniqueidentifier")]
    public Guid CartId { get; set; }

    [Required]
    [ForeignKey("Inventory")]
    [Column(TypeName = "varchar(6)")]
    public string InventoryId { get; set; } = string.Empty;

    [Required]
    public int Quantity { get; set; }
}