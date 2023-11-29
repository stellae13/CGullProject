using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Services.ServiceInterfaces;
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

        public async Task<bool> AddProduct(Product Product)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddItemToCart(Guid cartId, string itemId, int quantity)
        {
            var cart = await _context.Cart.FindAsync(cartId);

            if (cart == null)
            {
                throw new KeyNotFoundException($"Cart with ID {cartId} not found");
            }
            else if (quantity == 0)
            {
                throw new BadHttpRequestException($"Cannot add 0 items to cart");
            }

            var itemQuantity = from i in _context.Inventory
                               where i.Id == itemId
                               select i.Stock;

            if (quantity > itemQuantity.First())
            {
                throw new BadHttpRequestException($"Tring to add too many of this item. This item only has {itemQuantity} left in stock");
            }


            Dictionary<Tuple<Guid, String>, CartItem> cartItemTable =
                await _context.CartItem.ToDictionaryAsync<CartItem, Tuple<Guid, String>>(cItm => new(cItm.CartId, cItm.ProductId));
            Tuple<Guid, String> cartItemHandle = new(cartId, itemId);
            // If cart already has a quantity of this item, fetch its associated CartItem entry from the database
            // and then update its quantity to reflect the adjusted qty after adding this new qty to cart.
            if (cartItemTable.ContainsKey(cartItemHandle))
            {

                cartItemTable[new(cartId, itemId)].Quantity += quantity;
            }
            else
            {
                // Otherwise, make new entry in DB and add it to the CartItem DBSet
                CartItem cartItem = new CartItem
                {
                    CartId = cartId,
                    ProductId = itemId,
                    Quantity = quantity
                };
                await _context.CartItem.AddAsync(cartItem);

            }

            var item = from i in _context.Inventory
                       where i.Id == itemId
                       select i;


            item.First().Stock = item.First().Stock - quantity;
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> RemoveItemFromCart(Guid cartId, string itemId) {
            var record = await _context.CartItem.FindAsync(cartId, itemId);
            if (record == null) {
                return false;
            } else {
                _context.CartItem.Remove(record);
                await _context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            List<Category> categories = await _context.Category.ToListAsync();
            return categories;
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

        public async Task<IEnumerable<Product>> GetBundledProducts(String bundleId) 
        {
            if (bundleId[0] != '1')
                throw new BadHttpRequestException(
                    $"Bundle ID malformatted {bundleId}; missing bundle flag digit.");
            if (bundleId.Length != 6)
                throw new BadHttpRequestException(
                    $"Bundle ID malformatted {bundleId}.");
            Dictionary<String, Product> productTable = 
                await _context.Inventory.ToDictionaryAsync<Product, String>(p => p.Id);
            try
            {
                Bundle? bundle =
                    _context.Bundle.Where(b => b.ProductId == bundleId).Select(b => b).Include(b => b.BundleItems).First();

                IEnumerable<Product> ret =
                    from bndItm in bundle.BundleItems
                    where productTable.ContainsKey(bndItm.ProductId)
                    select productTable[bndItm.ProductId];
                return ret;
            }
            catch (InvalidOperationException e)
            {
                throw new KeyNotFoundException($"Bundle with ID {bundleId} not found", e);
            }

        }


        /*public async Task<IEnumerable<Bundle>> GetAssociatedBundles(String itemId)
        {
            if (itemId[0] != '0')
                throw new BadHttpRequestException($"ID {itemId} is a Bundle ID not an Item ID.");
            if (itemId.Length != 6)
                throw new BadHttpRequestException($"Item ID malformatted {itemId}.");
            IEnumerable<Bundle> ret = _context.Bundle.Where()

            return ret;
        }*/


        public async Task<IEnumerable<Product>> GetProductsByKeyword(String keywords)
        {
            HashSet<String> keySet = new(keywords.ToLower().Split("&"));
            return await 
                Task.Run((Func<IEnumerable<Product>>) 
                (() => {
                    IEnumerable<KeyValuePair<Product, int>> itemsAndRelevance =
                        from item in _context.Inventory
                        select new KeyValuePair<Product, int>(item, ScoreItemRelevance(item.Name.ToLower(), keySet));

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
