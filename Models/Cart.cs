using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject.Models
{
    public class Cart
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(32)")]
        public string Name { get; set; } = string.Empty;
    }
}
