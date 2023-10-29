using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject.Models
{
    [PrimaryKey(nameof(OrderId))]
    public class Order
    {
        [Required]
        [ForeignKey("Cart")]
        public Guid CartId { get; set; }


        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public DateTime OrderedOn { get; set; }

        public Address? Address { get; set; }

        //Honey, why did you spend 1,000,000 at SeagullMerch.com?

        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal total { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
