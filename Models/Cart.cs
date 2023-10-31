using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject.Models
{
    /// <summary>
    /// Model for shopping cart
    /// </summary>
    public class Cart
    {
        [Column(TypeName = "uniqueidentifier default NEWID()")]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(64)")]
        public string Name { get; set; } = string.Empty;

        public ICollection<CartItem> CartItems { get; set;}
        public ICollection<Review> Reviews { get; }
        public ICollection<Order> Orders { get; }
    }
}
