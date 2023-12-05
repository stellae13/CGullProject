using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CGullProject.Models;

namespace CGullProject;

/// <summary>
/// Table model for what Products are contained in what Bundle
/// </summary>
[PrimaryKey(nameof(BundleId), nameof(ItemId))]
public class BundleItem {
    [Required]
    [ForeignKey("Bundle")]
    public string BundleId { get; set; } = "";
    public Bundle Bundle { get; set; }

    [Required]
    [ForeignKey("Product")]
    [Column(TypeName = "varchar(6)")]
    public string ItemId { get; set; } = "";
    public Item Item { get; set; }
}