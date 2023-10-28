using System.Collections;

namespace CGullProject.Services
{
    public interface IProductService
    {
        public Task<bool> AddProduct(Inventory Product);
        public Task<bool> RemoveProduct(string productId);
        public Task<IEnumerable<Inventory>> GetAllProducts();

        /// <summary> Get products that match keywords provided, sorted by relevance </summary>
        public Task<IEnumerable<Inventory>> GetProductsByKeyword(String keywords);

        public Task<IEnumerable<Inventory>> GetProductsById(String ids);

        public Task<IEnumerable<Inventory>> GetProductsbyCategory(int categoryId);

        public Task<IEnumerable<Category>> GetAllCategories();


        public Task<bool> UpdateStock(string id, int newQuantity);

    }
}
