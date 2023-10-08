using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject;

public class Product {

    [Required]
    [StringLength(64, MinimumLength = 3)]
    public String Id { get; set; }

    [Required]
    [StringLength(32, MinimumLength = 2)]
    public string Name { get; set; }

    [ForeignKey("Category")]
    [Required]
    [StringLength(64, MinimumLength = 3)]
    public String CategoryId { get; set; }
    public Category Category { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    [Column(TypeName ="decimal(3,2)")]
    [Range(0,5)]
    public decimal Rating { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}