using System.ComponentModel.DataAnnotations;
using CGullProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return Ok(await _context.Inventory.ToListAsync());
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