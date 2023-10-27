using CGullProject.Models;

namespace CGullProject.Services
{
    public class CartService : ICartService
    {
        public Task<bool> AddItmeToCart(Guid cartID, string itemID, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetCart(Guid cartID)
        {
            throw new NotImplementedException();
        }

        public Task<TotalsDTO> GetTotals(Guid cartID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ProcessPayment(Guid cartID, string cardNumber, DateOnly exp, string cardHolderName, string cvc)
        {
            throw new NotImplementedException();
        }
    }
}
