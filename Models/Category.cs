using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject;

/// <summary>
/// Model for a Products category
/// </summary>
public class Category {
    
    [Required]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(32)")]
    public string Name { get; set; } = string.Empty;
}