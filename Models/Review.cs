using CGullProject.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject.Models
{
    /// <summary>
    /// Model for a review of an item
    /// </summary>
    [PrimaryKey(nameof(CartId), nameof(InventoryId))]
    public class Review
    {

        //this should be a user but the requirements have the cart acting as a user 
        [Required]
        [ForeignKey("Cart")]
        public Guid CartId{ get; set; }

        [Required]
        [ForeignKey("Inventory")]
        public string InventoryId { get; set; }

        [Required]
        [Column(TypeName = "decimal(3,2)")]
        [Range(0, 5)]
        public decimal Rating { get; set; }

        public string? Comment { get; set; }

        [Required]
        public DateTime Created {  get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        public Review()
        {

        }
        
        public Review(CreateReviewDTO reviewDTO, String id)
        {
            this.CartId = reviewDTO.CartId;
            this.InventoryId = id;
            this.Rating = reviewDTO.rating;
            this.Comment = reviewDTO.comment;
            this.Created = DateTime.Now;
            this.LastUpdated = DateTime.Now;
        }
    }
}
