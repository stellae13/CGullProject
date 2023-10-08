using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject;

[Owned]
[PrimaryKey(nameof(UserId),nameof(ProductId))]
public class CartItem {


    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    [ForeignKey("Product")]
    public string ProductId { get; set; }

    public Product Product { get; set; }

    [Required]
    [Range(0,int.MaxValue)]
    public int Quantity { get; set; }
}