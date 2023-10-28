using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CGullProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly ShopContext _context;

        public CartController(ShopContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        private readonly ICartService _cartService;

     

        // return details about the cart
        [HttpGet("GetCart")]
        public async Task<ActionResult> GetCart([Required] Guid cartId)
        {
            return NotFound();
        }

        // return the different totals for the cart
        [HttpGet("GetTotals")]
        public async Task<ActionResult> GetTotals([Required] Guid cartId)
        {
            // stubbed
            return NotFound();
        }


        [HttpPost("AddItemToCart")]
        public async Task<ActionResult> AddItemToCart([Required] Guid cartId, [Required] string itemId, [Required] int quantity)
        {
            var cart = await _context.Cart.FindAsync(cartId);

            if (cart == null)
            {
                return NotFound($"Cart with ID {cartId} not found");
            }
            else if (quantity == 0)
            {
                return BadRequest($"Cannot add 0 items to cart");
            }

            var itemQuantity = from i in _context.Inventory
                               where i.Id == itemId
                               select i.Stock;

            if (quantity > itemQuantity.First())
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
        [HttpPost("ProcessPayment")]
        public async Task<ActionResult> ProcessPayment([Required] Guid cartId, [Required] String cardNumber, [Required] DateOnly exp, [Required] String cardHolderName, [Required] String cvv)
        {

           ProcessPaymentDTO paymentInfo = new ProcessPaymentDTO(cartId,cardNumber, exp, cardHolderName, cvv);
            if (await _cartService.ProcessPayment(paymentInfo))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
