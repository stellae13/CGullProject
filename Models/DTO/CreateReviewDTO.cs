using System.ComponentModel.DataAnnotations;

namespace CGullProject.Models.DTO
{
    /// <summary>
    /// CreateReviewDTO
    /// </summary>
    public class CreateReviewDTO
    {

        /// <summary>
        /// CartId is the same as a user id
        /// </summary>
        [Required]
        public Guid CartId { get; set; }

        [Required]
        [Range(0, 5)]
        public decimal rating { get; set; }

        public string? comment { get; set; }

    }
}
