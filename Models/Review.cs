using CGullProject.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGullProject.Models
{
    /// <summary>
    /// Model for a review of an item
    /// </summary>
    [PrimaryKey(nameof(CartId), nameof(ItemId))]
    public class Review
    {

        //this should be a user but the requirements have the cart acting as a user 
        [Required]
        [ForeignKey("Cart")]
        public Guid CartId{ get; set; }

        [Required]
        [ForeignKey("Inventory")]
        public string ItemId { get; set; } = "";

        [Required]
        [Column(TypeName = "decimal(3,2)")]
        [Range(0, 5)]
        public decimal Rating { get; set; }

        public string? Comment { get; set; }

        [Required]
        public DateTime Created {  get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }
        
        public Review() {}

        public Review(CreateReviewDTO reviewDTO, string id)
        {
            CartId = reviewDTO.CartId;
            ItemId = id;
            Rating = reviewDTO.Rating;
            Comment = reviewDTO.Comment;
            Created = DateTime.Now;
            LastUpdated = DateTime.Now;
        }
    }
}
