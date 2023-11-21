using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Models.DTO;

namespace CGullProject.Services
{
    public class InventoryService : IInventoryService {

        private readonly ShopContext _context;

        public InventoryService(ShopContext context)
        {
            _context = context;
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
                isBundle = p.IsBundle
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

            // add to the quantity
            product.Stock += quantity;

            // save changes
            await _context.SaveChangesAsync();

            return true;
        }


    }

}