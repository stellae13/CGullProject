using System.ComponentModel.DataAnnotations;

namespace CGullProject.Models.DTO
{
    public class CreateReviewDTO
    {

        //this should be a user but the requirements have the cart acting as a user 
        [Required]
        public Guid CartId { get; set; }

        [Required]
        [Range(0, 5)]
        public decimal rating { get; set; }

        public string? comment { get; set; }

    }
}
