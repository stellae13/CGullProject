using System.ComponentModel.DataAnnotations;

namespace CGullProject.Models
{
    public class ProcessPaymentDTO
    {
        [Required]
        public Guid cartID { get; set; }

        [Required]
        public String cardNumber { get; set; }

        [Required]
        public DateOnly exp {  get; set; }

        [Required]
        public String cardHolderName { get; set; }

        [Required]
        public String cvv {  get; set; }
    }
}
