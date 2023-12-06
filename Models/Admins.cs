using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CGullProject.Models
{
    public class Admins
    {
        [Required]
        [Key]
        public string Username { get; set; } = "";

        [Required]
        [Column(TypeName = "binary(32)")]
        public byte[] Password { get; set; }

    }
}
