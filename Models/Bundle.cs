using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject;

public class Bundle {
    [Required]
    [Column(TypeName = "varchar(6)")]
    [ForeignKey("Inventory")]
    public string Id { get; set; } = "";
    public Product Product { get; set; }

    [Required]
    [Column(TypeName = "varchar(64)")]
    public string Name { get; set; } = "";


    [Required]
    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime EndDate { get; set; }

    public ICollection<BundleItem> BundleItems { get; set; }
}