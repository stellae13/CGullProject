using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject;

public class Bundle {
    [Required]
    [Column(TypeName = "varchar(6)")]
    [ForeignKey("Inventory")]
    public string Id { get; set; } = "";

    [Required]
    [Column(TypeName = "varchar(64)")]
    public string Name { get; set; } = "";

    [Required]
    [Column(TypeName = "decimal(3,2)")]
    public decimal Discount { get; set; }

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime EndDate { get; set; }
}