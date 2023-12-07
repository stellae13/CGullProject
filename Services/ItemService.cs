using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CGullProject.Services
{
    public class ItemService : IItemService
    {
        private readonly ShopContext _context;

        public ItemService(ShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            List<Category> categories = await _context.Category.ToListAsync();
            return categories;
        }

        public async Task<IEnumerable<Item>> GetItemsByCategory(int categoryId)
        {
            List<Item> items = new List<Item>();

            items = await _context.Inventory.Where( e => e.CategoryId == categoryId ).ToListAsync();
            return items;
        }

        public async Task<IEnumerable<Item>> GetItemsById(string itemIds)

        {

            // Request supplies string with ampersand-delim'd to easily split
            string[] ids = itemIds.Split("&");

            List<Item> itemsById = new();  // The sublist of items to return.

            Dictionary<string, Item> inventoryTable =
                await _context.Inventory.ToDictionaryAsync<Item, string>(itm => itm.Id);

            foreach (string id in ids)
            {
                // Tentative: skip over any requested Id that's malformatted
                // without returning bad request error.
                if (id.Length != 6)
                    continue;
                try
                {
                    Item itm = inventoryTable[id];
                    itemsById.Add(itm);
                }
                catch (KeyNotFoundException)
                {
                    // Tentative: skip over any requested Id that is not
                    // listed in DB without returning bad request error.
                    continue;
                }
            }
            return itemsById;
        }

        private static int ScoreItemRelevance(string itemName, HashSet<string> searchKeySet)
        {
            HashSet<string> tokensChecked = new();

            
            string[] nameTok = itemName.Split(" ");
            int ret = 0;
            foreach (string token in nameTok)
            {
                if (!tokensChecked.Add(token))
                    continue;
                if (searchKeySet.Contains(token))
                    ++ret;

            }

            return ret;
        }

        public async Task<IEnumerable<Item>> GetBundledItems(string bundleId) 
        {
            if (bundleId[0] != '1')
                throw new BadHttpRequestException(
                    $"Bundle ID malformatted {bundleId}; missing bundle flag digit.");
            if (bundleId.Length != 6)
                throw new BadHttpRequestException(
                    $"Bundle ID malformatted {bundleId}.");
            Dictionary<string, Item> itemTable = 
                await _context.Inventory.ToDictionaryAsync<Item, string>(p => p.Id);
            try
            {
                Bundle? bundle =
                    _context.Bundle.Where(b => b.ItemId == bundleId).Select(b => b).Include(b => b.BundleItems).First();

                IEnumerable<Item> ret =
                    from bndItm in bundle.BundleItems
                    where itemTable.ContainsKey(bndItm.ItemId)
                    select itemTable[bndItm.ItemId];
                return ret;
            }
            catch (InvalidOperationException e)
            {
                throw new KeyNotFoundException($"Bundle with ID {bundleId} not found", e);
            }

        }

        public async Task<IEnumerable<Item>> GetItemsByKeyword(string keywords)
        {
            HashSet<string> keySet = new(keywords.ToLower().Split("&"));
            return await
                Task.Run((Func<IEnumerable<Item>>)
                (() =>
                {
                    IEnumerable<KeyValuePair<Item, int>> itemsAndRelevance =
                        from item in _context.Inventory
                        select new KeyValuePair<Item, int>(item, ScoreItemRelevance(item.Name.ToLower(), keySet));

                    return
                        from kv in itemsAndRelevance
                        where kv.Value > 0
                        orderby kv.Value descending
                        select kv.Key;
                })
            );
        }
        public async Task<IEnumerable<Bundle>> GetAssociatedBundle(string itemId)
        {
            if (itemId[0] != '0')
                throw new BadHttpRequestException($"ID {itemId} is a Bundle ID not an Item ID.");
            if (itemId.Length != 6)
                throw new BadHttpRequestException($"Item ID malformatted {itemId}.");
            IEnumerable<Bundle> ret = await Task.Run(() => (from bndlItm in _context.BundleItem
                                      where bndlItm.ItemId == itemId
                                      select bndlItm.Bundle).ToArray());  // Wrap in Task.Run call to actually make use of the fact
                                                                          // that this is an asynchronous method


            return ret;
        }
    }

}
