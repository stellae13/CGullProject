using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject.Models
{
    [PrimaryKey(nameof(OrderId),nameof(InventoryId))]
    public class OrderItem
    {

        [Required]
        [ForeignKey("Order")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid OrderId { get; set; }

        [Required]
        [ForeignKey("Inventory")]
        public string InventoryId { get; set; } 
        public Inventory product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
