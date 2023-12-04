using CGullProject.Models;

namespace CGullProject.Services.ServiceInterfaces
{
    public interface IItemService
    {
        /// <summary>
        /// Add a new Item to the database
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>bool</returns>
        public Task<bool> AddItem(Item item);

        /// <summary>
        /// Remove an Item from the database
        /// </summary>
        /// <param name="itemId">string itemId</param>
        /// <returns>bool</returns>
        public Task<bool> RemoveItem(string itemId);
        
        /// <summary>
        /// Add an Item to a specific Cart
        /// </summary>
        /// <param name="cartId">Id of the cart</param>
        /// <param name="itemId">Id of the item</param>
        /// <param name="quantity">Number of the Item to add</param>
        /// <returns>bool</returns>
        public Task<bool> AddItemToCart(Guid cartId, string itemId, int quantity);

        /// <summary>
        /// Removes an item from a Cart.
        /// </summary>
        /// <param name="cartId">Id of the Cart</param>
        /// <param name="itemId">Id of the item</param>
        /// <returns>True iff item is found and successfully remove, false otherwise</returns>
        public Task<bool> RemoveItemFromCart(Guid cartId, string itemId);

        /// <summary>
        /// Get Items that match keywords provided, sorted by relevance.
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns><see cref="IEnumerable{Item}"/></returns>
        public Task<IEnumerable<Item>> GetItemsByKeyword(String keywords);

        /// <summary>
        /// Get a list of Items by a string of delimited Ids
        /// </summary>
        /// <param name="ids">String of delimited ids</param>
        /// <returns><see cref="IEnumerable{Item}"/></returns>
        public Task<IEnumerable<Item>> GetItemsById(String ids);

        /// <summary>
        /// Get a list of Items that belong to a specific category
        /// </summary>
        /// <param name="categoryId">Id of the category</param>
        /// <returns><see cref="IEnumerable{Item}"/></returns>
        public Task<IEnumerable<Item>> GetItemsByCategory(int categoryId);

        /// <summary>
        /// Get a list all categories from database
        /// </summary>
        /// <returns>IEnumerable&lt;Category&gt;</returns>
        public Task<IEnumerable<Category>> GetAllCategories();

        /// <summary>
        /// Get a list of Items that belong to a specific Bundle
        /// </summary>
        /// <param name="bundleIds">ID of the bundles</param>
        /// <returns><see cref="IEnumerable{Item}"/></returns>
        public Task<IEnumerable<Item>> GetBundledItems(String bundleIds);

        /*public Task<IEnumerable<Bundle>> GetAssociatedBundles(String itemId);*/

        /// <summary>
        /// Update the stock of a specific Item
        /// </summary>
        /// <param name="id">Id of the Item</param>
        /// <param name="newQuantity">New quanitty of the item</param>
        /// <returns></returns>
        public Task<bool> UpdateStock(string id, int newQuantity);

    }
}
