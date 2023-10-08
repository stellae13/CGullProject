using System.ComponentModel.DataAnnotations;

namespace CGullProject;

public class Category {
    [Required]
    [StringLength(64, MinimumLength = 3)]
    public String Id { get; set; }

    [Required]
    [StringLength(64, MinimumLength = 3)]
    public string Name { get; set; }
}