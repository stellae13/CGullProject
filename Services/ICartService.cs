

using CGullProject.Models;

namespace CGullProject.Services
{
    public interface ICartService
    {

        public Task<bool> AddItemToCart(Guid cartID, string itemID, int quantity);

        public Task<Cart> GetCart(Guid cartID);

        public Task<TotalsDTO> GetTotals(Guid cartID);

        public Task<bool> ProcessPayment(ProcessPaymentDTO paymentInfo);
    }
}
