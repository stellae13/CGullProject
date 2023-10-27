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

            // Filter out bundles by filtering out any product whose ID has
            // bundle flag digit set.
            products = 
                products.Where(prod => prod.Id[0] != '1').Select(prod => prod);
            return Ok(products);
        }
        
        [HttpGet("GetById")]
        public async Task<ActionResult> GetById([FromBody]String[] ids)
        {
            List<Inventory> matchingProds = new();
            Dictionary<String,Inventory> products = await _context.Inventory.ToDictionaryAsync<Inventory, String>(itm => itm.Id);
            foreach (string id in ids)
            {
                if (id.Length != 6) continue;
                Inventory? itm = products[id];
                if (itm is null) continue;
                matchingProds.Add(itm);
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