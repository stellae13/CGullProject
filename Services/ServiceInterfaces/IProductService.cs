using CGullProject.Models;

namespace CGullProject.Services.ServiceInterfaces
{
    public interface IProductService
    {
        public Task<bool> AddProduct(Product Product);
        public Task<bool> RemoveProduct(string productId);
        public Task<IEnumerable<Product>> GetAllProducts();

        /// <summary> Get products that match keywords provided, sorted by relevance </summary>
        public Task<IEnumerable<Product>> GetProductsByKeyword(String keywords);

        public Task<IEnumerable<Product>> GetProductsById(String ids);

        public Task<IEnumerable<Product>> GetProductsbyCategory(int categoryId);

        public Task<IEnumerable<Category>> GetAllCategories();

        public Task<IEnumerable<Product>> GetBundledProducts(String bundleIds);

        /*public Task<IEnumerable<Bundle>> GetAssociatedBundles(String itemId);*/

        public Task<bool> UpdateStock(string id, int newQuantity);

    }
}
