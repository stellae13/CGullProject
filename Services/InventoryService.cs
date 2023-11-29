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

        public async Task<IEnumerable<Product>> GetInventory()
        {
            IEnumerable<Product> inventory =
               await _context.Inventory.ToListAsync<Product>();


            return inventory;
        }

        public async Task<bool> AddNewItem(ProductDTO p)
        {
            // search for existing product
            Product? product = await _context.Inventory.FindAsync(p.Id);
            if (product != null) {
                return false;
            }

            // create new product
            Product newProduct = new() {
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
            await _context.Inventory.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangePrice(string itemId, decimal newPrice)
        {
            // search for the product in inventory
            Product? product = await _context.Inventory.FindAsync(itemId);
            if (product == null) {
                return false;
            }

            // set the new price
            product.MSRP = newPrice;
            product.SalePrice = newPrice;

            // save changes
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateStock(string itemId, int quantity)
        {
            // search for the product in inventory
            Product? product = await _context.Inventory.FindAsync(itemId);
            if (product == null) {
                return false;
            }

            // no negative stock values
            if (quantity >= 0) {
                product.Stock = quantity;

                // save changes
                await _context.SaveChangesAsync();
                return true;
            } else {
                return false;
            }
        }

        public async Task<bool> AdjustStock(string itemId, int amount) {
            // search for the product in inventory
            Product? product = await _context.Inventory.FindAsync(itemId);
            if (product == null) {
                return false;
            }

            // no negative stock values
            if (product.Stock + amount >= 0) {
                product.Stock += amount;

                // save changes
                await _context.SaveChangesAsync();
                return true;
            } else {
                return false;
            }
        }
        public async Task<IEnumerable<Product>> GetAllSalesItems()

        {
            IEnumerable<Product> inventory =
               await _context.Inventory.ToListAsync<Product>();

            List<Product> toReturn = new List<Product>();
            foreach (Product product in inventory)
            {
                if (product.OnSale)
                {
                    toReturn.Add(product);
                }
            }

            return toReturn;
        }
        public async Task<bool> ChangeSalesStatus(string itemId, bool status)
        {
            // search for the product in inventory
            Product? product = await _context.Inventory.FindAsync(itemId);
            if (product == null)
            {
                return false;
            }

            // change the status 
            product.OnSale = status;

            // save changes
            await _context.SaveChangesAsync();

            return true;
        }


    }

}