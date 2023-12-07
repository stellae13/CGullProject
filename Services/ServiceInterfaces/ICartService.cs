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
        /// <returns>Cart with the given Id</returns>
        public Task<CartDTO> GetCart(Guid cartId);

        /// <summary>
        /// Add an Item to a specific Cart
        /// </summary>
        /// <param name="cartId">Id of the cart</param>
        /// <param name="itemId">Id of the item</param>
        /// <param name="quantity">Number of the Item to add</param>
        /// <returns>Success/Failure</returns>
        public Task<bool> AddItemToCart(Guid cartId, string itemId, int quantity);

        /// <summary>
        /// Removes an item from a Cart.
        /// </summary>
        /// <param name="cartId">Id of the Cart</param>
        /// <param name="itemId">Id of the item</param>
        /// <returns>Success/Failure</returns>
        public Task<bool> RemoveItemFromCart(Guid cartId, string itemId);

        /// <summary>
        /// Get the totals of a particular Cart
        /// </summary>
        /// <param name="cartId">Id of the cart</param>
        /// <returns>Totals information</returns>
        public Task<TotalsDTO> GetTotals(Guid cartId);
        
        /// <summary>
        /// Process payment of a specific cart
        /// </summary>
        /// <param name="paymentInfo">Payment information</param>
        /// <returns>Success/Failure</returns>
        public Task<bool> ProcessPayment(ProcessPaymentDTO paymentInfo);

        /// <summary>
        /// Create a new Cart with a specific name
        /// </summary>
        /// <param name="cartName">Name of the cart (or user)</param>
        /// <returns>Guid of the Cart</returns>
        public Task<Guid> CreateNewCart(string cartName);

        /// <summary>
        /// Returns a list of Orders that belong to a specific Cart
        /// </summary>
        /// <param name="cartId">Guid of the cart</param>
        /// <returns>IEnumerable&lt;Order&gt;</returns>
        public Task<IEnumerable<Order>> GetOrdersById(Guid cartId);
    }
}
