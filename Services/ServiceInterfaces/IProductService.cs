using CGullProject.Models;
using System.Runtime.CompilerServices;

namespace CGullProject.Services.ServiceInterfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Add a new Product to the database
        /// </summary>
        /// <param name="Product">Product product</param>
        /// <returns>bool</returns>
        public Task<bool> AddProduct(Product Product);

        /// <summary>
        /// Remove a Product from the database
        /// </summary>
        /// <param name="productId">string productId</param>
        /// <returns>bool</returns>
        public Task<bool> RemoveProduct(string productId);

        /// <summary>
        /// Get all Products in the database
        /// </summary>
        /// <returns>IEnumerable&lt;Product&gt;</returns>
        public Task<IEnumerable<Product>> GetAllProducts();
        
        /// <summary>
        /// Add a Product to a specific Cart
        /// </summary>
        /// <param name="cartId">Id of the cart</param>
        /// <param name="itemId">Id of the product</param>
        /// <param name="quantity">Number of the product to add</param>
        /// <returns>bool</returns>
        public Task<bool> AddItemToCart(Guid cartId, string itemId, int quantity);

        /// <summary>
        /// Get products that match keywords provided, sorted by relevance.
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns>IEnumerable&lt;Product&gt;</returns>
        public Task<IEnumerable<Product>> GetProductsByKeyword(string keywords);

        /// <summary>
        /// Get a list of products by a string of delimited Ids
        /// </summary>
        /// <param name="ids">string of delimited ids</param>
        /// <returns>IEnumerable&lt;Product&gt;</returns>
        public Task<IEnumerable<Product>> GetProductsById(string ids);

        /// <summary>
        /// Get a list of Products that belong to a specific category
        /// </summary>
        /// <param name="categoryId">Id of the category</param>
        /// <returns>IEnumerable&lt;Product&gt;</returns>
        public Task<IEnumerable<Product>> GetProductsbyCategory(int categoryId);

        /// <summary>
        /// Get a list all categories from database
        /// </summary>
        /// <returns>IEnumerable&lt;Category&gt;</returns>
        public Task<IEnumerable<Category>> GetAllCategories();

        /// <summary>
        /// Get a list of Products that belong to a specific Bundle
        /// </summary>
        /// <param name="bundleIds">ID of the bundles</param>
        /// <returns>IEnumerable&lt;Product&gt;</returns>
        public Task<IEnumerable<Product>> GetBundledProducts(string bundleIds);

        /// <summary>
        /// Update the stock of a specific product
        /// </summary>
        /// <param name="id">Id of the product</param>
        /// <param name="newQuantity">New quanitty of the product</param>
        /// <returns></returns>
        public Task<bool> UpdateStock(string id, int newQuantity);

        /// <summary>
        /// Get all bundles associated with a specific Product
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>IEnumerable&lt;Bundle&gt;</returns>
        public Task<IEnumerable<Bundle>> GetAssociatedBundles(string itemId);

    }
}
