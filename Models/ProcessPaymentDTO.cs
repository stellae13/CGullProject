using System.ComponentModel.DataAnnotations;

namespace CGullProject.Models
{
    public class ProcessPaymentDTO
    {

       public  ProcessPaymentDTO(Guid cartID, string cardNumber, DateOnly exp, string cardHolderName, string cvv)
        {
            this.cartID = cartID;
            this.cardNumber = cardNumber;
            this.exp = exp;
            this.cardHolderName = cardHolderName;
            this.cvv = cvv;
        }

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
