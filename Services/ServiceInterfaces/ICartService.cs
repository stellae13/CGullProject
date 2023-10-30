using CGullProject.Models;
using CGullProject.Models.DTO;

namespace CGullProject.Services.ServiceInterfaces
{
    public interface ICartService
    {

        

        /// <summary>
        /// Get the cart instance, as well as a list of tuples that show all items in cart (by ID), the quantity of ea. respective item,
        /// and their respective running total.
        /// </summary>
        /// <param name="cartId"> The ID of the cart whose relevant data the controller is requesting </param>
        /// <returns></returns>
        public Task<CartDTO> GetCart(Guid cartId);

        public Task<TotalsDTO> GetTotals(Guid cartId);
        public Task<bool> ProcessPayment(ProcessPaymentDTO paymentInfo);

        public Task<Guid> CreateNewCart(string cartName);

        public Task<IEnumerable<Order>> GetOrdersById(Guid CartId);
    }
}
