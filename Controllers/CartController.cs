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
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

  
        // return details about the cart
        [HttpGet("GetCart")]
        public async Task<ActionResult> GetCart([Required] Guid cartId)
        {
            try
            {
                Tuple<Cart, IEnumerable<CartItemView>> cart = await _cartService.GetCart(cartId);
                return Ok(cart);
            } catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpPost("CreateNewCart")]
        public async Task<ActionResult> CreateNewCart(String name)
        {
            Guid newCartId = await _cartService.CreateNewCart(name);
            return Ok(newCartId);

        }

        // return the different totals for the cart
        [HttpGet("GetTotals")]
        public async Task<ActionResult> GetTotals([Required] Guid cartId)
        {
            throw new NotImplementedException();  // Ill finish this tomorrow I'm so tired. its 5 am i think lol
            
        }


        [HttpPost("AddItemToCart")]
        public async Task<ActionResult> AddItemToCart([Required] Guid cartId, [Required] string itemId, [Required] int quantity)
        {
            try
            {
                await _cartService.AddItemToCart(cartId, itemId, quantity);
                return Ok($"Product with ID {itemId} added to cart with ID {cartId}.");
            } catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            } catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }

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
