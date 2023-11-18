using CGullProject.Models;

namespace CGullProject.Services
{
    public interface IInventoryService {
        
        /// <summary>
        /// Adds quantity to the current stock of the Product
        /// </summary>
        /// <param name="itemId">Id of the Product</param>
        /// <param name="quantity">Amount to increase or decrease the stock by</param>
        /// <returns>True iff successful, false otherwise</returns>
        public Task<bool> UpdateStock(string itemId, int quantity);

        /// <summary>
        /// Changes the MSRP and SalePrice of a Product
        /// </summary>
        /// <param name="itemId">Id of the Product</param>
        /// <param name="newPrice">New price for the Product</param>
        /// <returns>True iff successful, false otherwise</returns>
        public Task<bool> ChangePrice(string itemId, decimal newPrice);

        /// <summary>
        /// Add a new Product to the Inventory
        /// </summary>
        /// <param name="p">Product to add</param>
        /// <returns>True iff successful, false otherwise</returns>
        public Task<bool> AddNewItem(Product p);

    }
}