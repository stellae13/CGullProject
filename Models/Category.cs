using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject;

public class Category {
    
    [Required]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(64)")]
    public string Name { get; set; } = string.Empty;
}