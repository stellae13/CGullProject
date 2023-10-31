using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject;

/// <summary>
/// Model for a Bundle
/// </summary>
public class Bundle {
    [Required]
    [Key]
    public string ProductId { get; set; } 

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime EndDate { get; set; }

    public ICollection<BundleItem> BundleItems { get; set; }
}