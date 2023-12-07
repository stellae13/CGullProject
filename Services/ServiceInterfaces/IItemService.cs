using CGullProject.Models;

namespace CGullProject.Services.ServiceInterfaces
{
    public interface IItemService
    {
        /// <summary>
        /// Get Items that match keywords provided, sorted by relevance.
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns><see cref="IEnumerable{Item}"/></returns>
        public Task<IEnumerable<Item>> GetItemsByKeyword(string keywords);

        /// <summary>
        /// Get a list of Items by a string of delimited Ids
        /// </summary>
        /// <param name="ids">String of delimited ids</param>
        /// <returns><see cref="IEnumerable{Item}"/></returns>
        public Task<IEnumerable<Item>> GetItemsById(string ids);

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
        public Task<IEnumerable<Item>> GetBundledItems(string bundleIds);


        /// <summary>
        /// Get associated bundle. 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>IEnumerable&lt;Bundle&gt;</returns>
        public Task<IEnumerable<Bundle>> GetAssociatedBundle(string itemId);

    }
}
