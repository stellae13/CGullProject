using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CGullProject;

[PrimaryKey(nameof(BundleId), nameof(ProductId))]
public class BundleItem {
    [Required]
    public string BundleId { get; set; } = "";

    [Required]
    public string ProductId { get; set; } = "";
}