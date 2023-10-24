using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject;

public class Bundle {
    [Required]
    [Column(TypeName = "varchar(64)")]
    public string Id { get; set; } = "";

    [Required]
    [Column(TypeName = "decimal(3,2)")]
    public decimal Discount { get; set; }

    [Required]
    [Column(TypeName = "varchar(64)")]
    public string Name { get; set; } = "";
}