using System.ComponentModel.DataAnnotations;

namespace CGullProject;

public class Bundle {
    [Required]
    public string Id { get; set; } = "";

    [Required]
    public decimal Discount { get; set; }

    [Required]
    public string Name { get; set; } = "";
}