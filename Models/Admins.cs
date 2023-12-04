using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CGullProject.Models
{
    public class Admins
    {
        [Required]
        [Key]
        public string Username { get; set; }

        [Required]
        [Unicode(false)]
        [MaxLength(32)]
        public string Password { get; set; }

    }
}
