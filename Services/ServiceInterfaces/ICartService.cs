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
        /// <param name="cartId">Id of the cart</param>
        /// <returns>CartDTO</returns>
        public Task<CartDTO> GetCart(Guid cartId);

        /// <summary>
        /// Get the totals of a particular Cart
        /// </summary>
        /// <param name="cartId">Id of the cart</param>
        /// <returns>TotalsDTO</returns>
        public Task<TotalsDTO> GetTotals(Guid cartId);
        
        /// <summary>
        /// Process payment of a specific cart
        /// </summary>
        /// <param name="paymentInfo">ProcessPaymentDTO</param>
        /// <returns>bool</returns>
        public Task<bool> ProcessPayment(ProcessPaymentDTO paymentInfo);

        /// <summary>
        /// Create a new Cart with a specific name
        /// </summary>
        /// <param name="cartName">Name of the cart (or user)</param>
        /// <returns>Guid of the Cart</returns>
        public Task<Guid> CreateNewCart(string cartName);

        /// <summary>
        /// Returns a list of Orders that belong to a specific Cart (or user)
        /// </summary>
        /// <param name="CartId">Guid of the cart</param>
        /// <returns>IEnumerable&lt;Order&gt;</returns>
        public Task<IEnumerable<Order>> GetOrdersById(Guid CartId);
    }
}
