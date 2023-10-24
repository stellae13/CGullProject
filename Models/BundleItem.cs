using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject;

[PrimaryKey(nameof(BundleId), nameof(InventoryId))]
public class BundleItem {
    [Required]
    [ForeignKey("Bundle")]
    [Column(TypeName = "varchar(6)")]
    public string BundleId { get; set; } = "";

    [Required]
    [ForeignKey("Inventory")]
    [Column(TypeName = "varchar(6)")]
    public string InventoryId { get; set; } = "";
}