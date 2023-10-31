using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject;

/// <summary>
/// Table model for what Products are contained in what Cart
/// </summary>
[Owned]
[PrimaryKey(nameof(CartId),nameof(ProductId))]
public class CartItem {

    [Required]
    [Column(TypeName = "uniqueidentifier")]
    [ForeignKey("Cart")]
    public Guid CartId { get; set; }

    [Required]
    [ForeignKey("Product")]
    [Column(TypeName = "varchar(6)")]
    public string ProductId { get; set; } = string.Empty;

    [Required]
    public int Quantity { get; set; }
}