using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CGullProject.Models
{
    public class Admins
    {
        [Required]
        [Key]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
