using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CGullProject.Services
{
    public class InventoryService : IInventoryService {

        private readonly ShopContext _context;

        public InventoryService(ShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetInventory()
        {
            IEnumerable<Item> inventory =
               await _context.Inventory.ToListAsync<Item>();


            return inventory;
        }

        public async Task<bool> AddNewItem(ItemDTO p)
        {
            // search for existing item
            Item? item = await _context.Inventory.FindAsync(p.Id);
            if (item != null) {
                return false;
            }

            // create new item
            Item newItem = new() {
                Id = p.Id,
                Name = p.Name,
                CategoryId = p.CategoryId,
                MSRP = p.MSRP,
                SalePrice = p.SalePrice,
                Rating = p.Rating,
                Stock = p.Stock,
                IsBundle = p.IsBundle
            };

            // add and save changes
            await _context.Inventory.AddAsync(newItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangePrice(string itemId, decimal newPrice)
        {
            // search for the item in inventory
            Item? item = await _context.Inventory.FindAsync(itemId);
            if (item == null) {
                return false;
            }

            // set the new price
            item.MSRP = newPrice;
            item.SalePrice = newPrice;

            // save changes
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateStock(string itemId, int quantity)
        {
            // search for the item in inventory
            Item? item = await _context.Inventory.FindAsync(itemId);
            if (item == null) {
                return false;
            }

            // no negative stock values
            if (quantity >= 0) {
                item.Stock = quantity;

                // save changes
                await _context.SaveChangesAsync();
                return true;
            } else {
                return false;
            }
        }

        public async Task<bool> AdjustStock(string itemId, int amount) {
            // search for the item in inventory
            Item? item = await _context.Inventory.FindAsync(itemId);
            if (item == null) {
                return false;
            }

            // no negative stock values
            if (item.Stock + amount >= 0) {
                item.Stock += amount;

                // save changes
                await _context.SaveChangesAsync();
                return true;
            } else {
                return false;
            }
        }
        public async Task<IEnumerable<Item>> GetAllSalesItems()

        {
            IEnumerable<Item> inventory =
               await _context.Inventory.ToListAsync<Item>();

            List<Item> toReturn = new List<Item>();
            foreach (Item item in inventory)
            {
                if (item.OnSale)
                {
                    toReturn.Add(item);
                }
            }

            return toReturn;
        }
        public async Task<bool> ChangeSalesStatus(string itemId, bool status)
        {
            // search for the item in inventory
            Item? item = await _context.Inventory.FindAsync(itemId);
            if (item == null)
            {
                return false;
            }

            // change the status 
            item.OnSale = status;

            // save changes
            await _context.SaveChangesAsync();

            return true;
        }


    }

}