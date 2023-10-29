using CGullProject.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject;

public class Product {

    [Required]
    [Column(TypeName = "varchar(6)")]
    public string Id { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "varchar(32)")]
    public string Name { get; set; } = string.Empty;

    [ForeignKey("Category")]
    [Required]
    public int CategoryId { get; set; }

    public Category Category { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal MSRP { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal SalePrice { get; set; }

    [Required]
    [Column(TypeName = "decimal(3,2)")]
    [Range(0, 5)]
    public decimal Rating { get; set; }

    [Required]
    [Column(TypeName = "int")]
    public int Stock { get; set; }

    [Required]
    public bool isBundle {  get; set; }

    public ICollection<Review> Reviews { get; }
}