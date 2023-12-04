using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject.Models
{
    /// <summary>
    /// Model for an order history
    /// </summary>
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
        //Because, sweetie, there was a really good sale!!!

        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        public Order() {}

        public Order(Guid cartId, Guid orderId, DateTime orderedOn,  decimal total)
        {
            CartId = cartId;
            OrderId = orderId;
            OrderedOn = orderedOn;
            Total = total;
        }
    }
}
