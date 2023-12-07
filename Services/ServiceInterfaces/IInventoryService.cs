using CGullProject.Models;
using CGullProject.Models.DTO;

namespace CGullProject.Services
{
    public interface IInventoryService {

        /// <summary>
        /// Get all items in the database
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Item>> GetInventory();

        /// <summary>
        /// Sets current stock of the <see cref="Item"/> to quantity
        /// </summary>
        /// <param name="itemId">Id of the <see cref="Item"/></param>
        /// <param name="quantity">Number of <see cref="Item"/> in stock</param>
        /// <returns>Success/Failure</returns>
        public Task<bool> UpdateStock(string itemId, int quantity);

        /// <summary>
        /// Adds amount to the stock of the <see cref="Item"/>
        /// </summary>
        /// <param name="itemId">Id of the <see cref="Item"/></param>
        /// <param name="amount">Amount to increase(+) or decrease(-) the stock by</param>
        /// <returns>Success/Failure</returns>
        public Task<bool> AdjustStock(string itemId, int amount);

        /// <summary>
        /// Changes the MSRP and SalePrice of a <see cref="Item"/>
        /// </summary>
        /// <param name="itemId">Id of the <see cref="Item"/></param>
        /// <param name="newPrice">New price for the <see cref="Item"/></param>
        /// <returns>Success/Failure</returns>
        public Task<bool> ChangePrice(string itemId, decimal newPrice);

        /// <summary>
        /// Add a new <see cref="Item"/> to the Inventory
        /// </summary>
        /// <param name="item"><see cref="Item"/> to add</param>
        /// <returns>Success/Failure</returns>
        public Task<bool> AddNewItem(ItemDTO item);

        /// <summary>
        /// Get all items on sale
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Item>> GetAllSalesItems();

        /// <summary>
        /// Changes the sale status of an item to either true or false
        /// </summary>
        /// <param name="itemID">Id of the item to change</param>
        /// <param name="status">New sale status for the item</param>
        /// <returns>Success/Failure</returns>
        public Task<bool> ChangeSalesStatus(string itemId, bool status);

       



    }
}