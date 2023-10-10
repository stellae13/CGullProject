using System.ComponentModel.DataAnnotations;

namespace CGullProject.Models
{
    public class Cart
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 1)]
        public string? Name { get; set; }    

        public ICollection<CartItem> Items { get; set;} = new List<CartItem>();
    }
}
