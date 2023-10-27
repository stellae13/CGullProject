using CGullProject.Data;
using Microsoft.EntityFrameworkCore;

namespace CGullProject.Services
{
    public class ProductService : IProductService
       
    {
        private readonly ShopContext _context;

        public ProductService(ShopContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProduct(Inventory Product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            List<Category> categories = await _context.Category.ToListAsync();
            return categories;
        }

        public async Task<IEnumerable<Inventory>> GetAllProducts()
        {
            IEnumerable<Inventory> inventory =
               await _context.Inventory.ToListAsync<Inventory>();

            // Query to filter out bundles
            inventory =
                from item in inventory where item.Id[0] == '0' select item;
            return inventory;
        }

        public async Task<IEnumerable<Inventory>> GetProductsbyCategory(int categoryId)
        {
            List<Inventory> products = new List<Inventory>();

            products = await _context.Inventory.Where( e => e.CategoryId == categoryId ).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Inventory>> GetProductsById(string productIDs)

        {

            // Request supplies string with ampersand-delim'd to easily split
            String[] ids = productIDs.Split("&");

            List<Inventory> itemsById = new();  // The sublist of items to return.

            Dictionary<String, Inventory> inventoryTable =
                await _context.Inventory.ToDictionaryAsync<Inventory, String>(itm => itm.Id);

            foreach (string id in ids)
            {
                // Tentative: skip over any requested Id that's malformatted
                // without returning bad request error.
                if (id.Length != 6)
                    continue;
                try
                {
                    Inventory itm = inventoryTable[id];
                    itemsById.Add(itm);
                }
                catch (KeyNotFoundException e)
                {
                    // Tentative: skip over any requested Id that is not
                    // listed in DB without returning bad request error.
                    continue;
                }
            }
            return itemsById;
        }

        public async Task<bool> RemoveProduct(string productId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateStock(string id, int newQuantity)
        {
            throw new NotImplementedException();
        }
    }
}
