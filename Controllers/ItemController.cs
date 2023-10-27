using System.ComponentModel.DataAnnotations;
using CGullProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CGullProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase {

        private readonly ShopContext _context;

        public ItemController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllItems")]
        public async Task<ActionResult> GetAllItems()
        {
            IEnumerable<Inventory> products = 
                await _context.Inventory.ToListAsync<Inventory>();                
            // Returns query result that filters out bundles by filtering out any product whose ID
            return
                Ok(products.Where(entry => entry.Id[0] == '0').Select(entry => entry));
        }
        
        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(String idList)
        {
            // Request supplies string with ampersand-delim'd to easily split
            String[] ids = idList.Split("&");
            List<Inventory> matchingProds = new();
            Dictionary<String,Inventory> products = await _context.Inventory.ToDictionaryAsync<Inventory, String>(itm => itm.Id);
            foreach (string id in ids)
            {
                // Tentative: skip over any requested Id that's malformatted
                // without returning bad request error.
                if (id.Length != 6)
                    continue;
                try
                {
                    Inventory itm = products[id];
                    matchingProds.Add(itm);
                }
                catch (KeyNotFoundException e)
                {
                    // Tentative: skip over any requested Id that is not
                    // listed in DB without returning bad request error.
                    continue;
                }
            }
            return Ok(matchingProds);
        }

        

        [HttpPost("AddItemToCart")]
        public async Task<ActionResult> AddItemToCart([Required] Guid cartId, [Required] string itemId, [Required] int quantity) {
            var cart = await _context.Cart.FindAsync(cartId);

            if (cart == null)
            {
                return NotFound($"Cart with ID {cartId} not found");
            }

            CartItem cartItem = new CartItem
            {
                CartId = cartId,
                InventoryId = itemId,
                Quantity = quantity
            };

            await _context.CartItem.AddAsync(cartItem);
            await _context.SaveChangesAsync();

            return Ok($"Product with ID {itemId} added to cart with ID {cartId}.");
        }
    }
}