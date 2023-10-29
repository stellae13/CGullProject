using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace CGullProject.Services
{
    public class ProductService : IProductService
       
    {
        private readonly ShopContext _context;

        public ProductService(ShopContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProduct(Product Product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            List<Category> categories = await _context.Category.ToListAsync();
            return categories;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            IEnumerable<Product> inventory =
               await _context.Inventory.ToListAsync<Product>();

         
            return inventory;
        }

        public async Task<IEnumerable<Product>> GetProductsbyCategory(int categoryId)
        {
            List<Product> products = new List<Product>();

            products = await _context.Inventory.Where( e => e.CategoryId == categoryId ).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsById(string productIDs)

        {

            // Request supplies string with ampersand-delim'd to easily split
            String[] ids = productIDs.Split("&");

            List<Product> itemsById = new();  // The sublist of items to return.

            Dictionary<String, Product> inventoryTable =
                await _context.Inventory.ToDictionaryAsync<Product, String>(itm => itm.Id);

            foreach (string id in ids)
            {
                // Tentative: skip over any requested Id that's malformatted
                // without returning bad request error.
                if (id.Length != 6)
                    continue;
                try
                {
                    Product itm = inventoryTable[id];
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

        private static int ScoreItemRelevance(String itemName, HashSet<String> searchKeySet)
        {
            HashSet<String> tokensChecked = new();

            // Filter out any punctuation
            String[] nameTok = itemName.Split(" ");
            int ret = 0;
            foreach (String token in nameTok)
            {
                if (!tokensChecked.Add(token))
                    continue;
                if (searchKeySet.Contains(token))
                    ++ret;

            }

            return ret;
        }
        
        public async Task<IEnumerable<Product>> GetProductsByKeyword(String keywords)
        {
            HashSet<String> keySet = new(keywords.Split("&"));
            return await 
                Task.Run((Func<IEnumerable<Product>>) 
                (() => {
                    IEnumerable<KeyValuePair<Product, int>> itemsAndRelevance =
                        from item in _context.Inventory
                        select new KeyValuePair<Product, int>(item, ScoreItemRelevance(item.Name, keySet));

                    return
                        from kv in itemsAndRelevance
                        where kv.Value > 0
                        orderby kv.Value descending
                        select kv.Key;
                })
            );
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
