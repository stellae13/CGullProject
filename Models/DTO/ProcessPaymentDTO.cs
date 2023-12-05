using System.ComponentModel.DataAnnotations;

namespace CGullProject.Models.DTO
{
    /// <summary>
    /// ProcessPaymentDTO
    /// </summary>
    public class ProcessPaymentDTO
    {

        public ProcessPaymentDTO(Guid cartID, string cardNumber, DateOnly exp, string cardHolderName, string cvv)
        {
            CartId = cartID;
            CardNumber = cardNumber;
            Exp = exp;
            CardHolderName = cardHolderName;
            Cvv = cvv;
        }

        [Required]
        public Guid CartId { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public DateOnly Exp { get; set; }

        [Required]
        public string CardHolderName { get; set; }

        [Required]
        public string Cvv { get; set; }
    }
}
