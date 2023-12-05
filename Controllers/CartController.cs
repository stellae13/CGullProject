using CGullProject.Data;
using CGullProject.Models.DTO;
using CGullProject.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using CGullProject.Models;

namespace CGullProject.Controllers
{
    /// <summary>
    /// Stores the <see cref="Cart"/> endpoints
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        /// <summary>
        /// <see cref="ICartService"/> that the controller will use to perform <see cref="Cart"/> related data operations
        /// </summary>
        private readonly ICartService _cartService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cartService">Target <see cref="ICartService"/></param>
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

  
        /// <summary>
        /// Returns all details, including the totals, of a specific <see cref="Cart"/>
        /// </summary>
        /// <param name="cartId"><see cref="Guid"/>  of the <see cref="Cart"/> to get the details of</param>
        /// <returns><see cref="CartDTO"/> details of the cart</returns>
        [HttpGet("GetCart")]
        public async Task<ActionResult<CartDTO>> GetCart([Required] Guid cartId)
        {
            try
            {
                CartDTO cart = await _cartService.GetCart(cartId);
                return Ok(cart);
            } catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Add a new <see cref="Cart"/> to the database with specified name
        /// </summary>
        /// <param name="name">Name of the cart</param>
        /// <returns><see cref="Guid"/> of the newly created <see cref="Cart"/></returns>
        [HttpPost("CreateNewCart")]
        public async Task<ActionResult<Guid>> CreateNewCart(string name)
        {
            Guid newCartId = await _cartService.CreateNewCart(name);
            return Ok(newCartId);

        }

        /// <summary>
        /// Get the different totals of the <see cref="Cart"/>  (Regular Total, Bundle Total, Total with Tax)
        /// </summary>
        /// <param name="cartId"><see cref="Guid"/> of the <see cref="Cart"/> to get the totals of</param>
        /// <returns>TotalsDTO</returns>
        [HttpGet("GetTotals")]
        public async Task<ActionResult<TotalsDTO>> GetTotals([Required] Guid cartId)
        {
            try
            {
                TotalsDTO totals = await _cartService.GetTotals(cartId);
                return Ok(totals);
            } catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Validates a payment method
        /// </summary>
        /// <param name="cartId">Id of the cart to pay for</param>
        /// <param name="cardNumber">Card number</param>
        /// <param name="exp">Expiration date</param>
        /// <param name="cardHolderName">Name of card holder</param>
        /// <param name="cvv">CVV (also known as CVC, security code)</param>
        /// <returns>Payment details</returns>
        [HttpPost("ProcessPayment")]
        public async Task<ActionResult<ProcessPaymentDTO>> ProcessPayment([Required] Guid cartId, [Required] string cardNumber, [Required] DateOnly exp, [Required] String cardHolderName, [Required] String cvv)
        {
            ProcessPaymentDTO paymentInfo = new ProcessPaymentDTO(cartId,cardNumber, exp, cardHolderName, cvv);
            if (await _cartService.ProcessPayment(paymentInfo))
            {
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Returns a list of <see cref="Order"/>s that are made by a user
        /// </summary>
        /// <param name="Id">User id</param>
        /// <returns><see cref="IEnumerable{Order}"/> </returns>
        [HttpGet("{Id}/Orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersById(Guid Id)
        {
            return Ok(await _cartService.GetOrdersById(Id));
        }
    }
}
