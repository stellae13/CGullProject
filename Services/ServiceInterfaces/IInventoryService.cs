using CGullProject.Models;
using CGullProject.Models.DTO;

namespace CGullProject.Services
{
    public interface IInventoryService {

        /// <summary>
        /// Gets the Inventory (all items)
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Product>> GetInventory();

        /// <summary>
        /// Sets current stock of the Product to quantity
        /// </summary>
        /// <param name="itemId">Id of the Product</param>
        /// <param name="quantity">Number of Product in stock</param>
        /// <returns>True iff successful, false otherwise</returns>
        public Task<bool> UpdateStock(string itemId, int quantity);

        /// <summary>
        /// Adds amount to the stock of the Product
        /// </summary>
        /// <param name="itemId">Id of the Product</param>
        /// <param name="amount">Amount to increase(+) or decrease(-) the stock by</param>
        /// <returns>True iff successful, false otherwise</returns>
        public Task<bool> AdjustStock(string itemId, int amount);

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
        /// <param name="product">Product to add</param>
        /// <returns>True iff successful, false otherwise</returns>
        public Task<bool> AddNewItem(ProductDTO product);

        /// <summary>
        /// returns all of the items on sale
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Product>> GetAllSalesItems();

        /// <summary>
        /// Changes the sale status of an item to either true or false
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Task<bool> ChangeSalesStatus(string itemId, bool status);

       



    }
}