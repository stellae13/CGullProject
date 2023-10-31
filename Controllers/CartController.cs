using CGullProject.Data;
using CGullProject.Models.DTO;
using CGullProject.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CGullProject.Controllers
{
    /// <summary>
    /// Stores the Cart endpoints
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        /// <summary>
        /// ICartService that the controller will use to perform Cart related data operations.
        /// </summary>
        private readonly ICartService _cartService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cartService">Target ICartService</param>
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

  
        /// <summary>
        /// Returns all cart details, including the totals, of a specific cart.
        /// </summary>
        /// <param name="cartId">Guid of the cart to get the details of</param>
        /// <returns>CartDTO</returns>
        [HttpGet("GetCart")]
        public async Task<ActionResult> GetCart([Required] Guid cartId)
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
        /// Add a new cart to the database with specified name.
        /// </summary>
        /// <param name="name">Name of the cart to add.</param>
        /// <returns>Guid</returns>
        [HttpPost("CreateNewCart")]
        public async Task<ActionResult> CreateNewCart(String name)
        {
            Guid newCartId = await _cartService.CreateNewCart(name);
            return Ok(newCartId);

        }

        /// <summary>
        /// Get the different totals of the Cart (Regular Total, Bundle Total, Total with Tax).
        /// </summary>
        /// <param name="cartId">Guid of the Cart to get the totals of.</param>
        /// <returns>TotalsDTO</returns>
        [HttpGet("GetTotals")]
        public async Task<ActionResult> GetTotals([Required] Guid cartId)
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
        /// Validates a payment method.
        /// </summary>
        /// <param name="cartId">Guid of the cart to 'pay for'.</param>
        /// <param name="cardNumber">String card number</param>
        /// <param name="exp">DateOnly expiry date</param>
        /// <param name="cardHolderName">String name of card holder</param>
        /// <param name="cvv">String cvv</param>
        /// <returns>ActionResult</returns>
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

        /// <summary>
        /// Returns a list of orders that are made by user with id.
        /// </summary>
        /// <param name="Id">Guid user id.</param>
        /// <returns>IEnumerable&lt;Order&gt;</returns>
        [HttpGet("{Id}/Orders")]
        public async Task<ActionResult> GetOrdersById(Guid Id)
        {
            return Ok(await _cartService.GetOrdersById(Id));
        }
    }
}
