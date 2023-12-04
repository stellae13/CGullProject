using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject.Models
{
    /// <summary>
    /// Table model for when an order was placed on a specific product
    /// </summary>
    [PrimaryKey(nameof(OrderId),nameof(ItemId))]
    public class OrderItem
    {

        [Required]
        [ForeignKey("Order")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid OrderId { get; set; }

        [Required]
        [ForeignKey("Inventory")]
        public string ItemId { get; set; } = "";

        public Item Item { get; set; } = new Item();

        [Required]
        public int Quantity { get; set; }

        public OrderItem() {}

        public OrderItem(Guid orderId, string productId, int quantity)
        {
            OrderId = orderId;
            ItemId = productId;
            Quantity = quantity;
        }
    }
}
