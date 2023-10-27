

using CGullProject.Models;

namespace CGullProject.Services
{
    public interface ICartService
    {

        public Task<bool> AddItmeToCart(Guid cartID, string itemID, int quantity);

        public Task<Cart> GetCart(Guid cartID);

        public Task<TotalsDTO> GetTotals(Guid cartID);

        public Task<bool> ProcessPayment(Guid cartID, String cardNumber, DateOnly exp, string cardHolderName, string cvc);
    }
}
