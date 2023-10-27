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
            IEnumerable<Inventory> inventory =
                await _context.Inventory.ToListAsync<Inventory>();

            // Query to filter out bundles
            inventory =
                from item in inventory where item.Id[0] == '0' select item;

            return Ok(inventory);
        }
        
        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(String idList)
        {
            // Request supplies string with ampersand-delim'd to easily split
            String[] ids = idList.Split("&");
            
            List<Inventory> itemsById = new();  // The sublist of items to return.

            Dictionary<String,Inventory> inventoryTable = 
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
            return Ok(itemsById);
        }

        

        [HttpPost("AddItemToCart")]
        public async Task<ActionResult> AddItemToCart([Required] Guid cartId, [Required] string itemId, [Required] int quantity) {
            var cart = await _context.Cart.FindAsync(cartId);

            if (cart == null)
            {
                return NotFound($"Cart with ID {cartId} not found");
            } else if (quantity == 0)
            {
                return BadRequest($"Cannot add 0 items to cart");
            }

            var itemQuantity = from i in _context.Inventory
                               where i.Id == itemId
                               select i.Stock;

            if(quantity > itemQuantity.First())
            {
                return BadRequest($"Tring to add too many of this item. This item only has {itemQuantity} left in stock");
            }

            CartItem cartItem = new CartItem
            {
                CartId = cartId,
                InventoryId = itemId,
                Quantity = quantity
            };

            var item = from i in _context.Inventory
                       where i.Id == itemId
                       select i;

            await _context.CartItem.AddAsync(cartItem);
            item.First().Stock = item.First().Stock - quantity;
            await _context.SaveChangesAsync();

            return Ok($"Product with ID {itemId} added to cart with ID {cartId}.");
        }
    }
}