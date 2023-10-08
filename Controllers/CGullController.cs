using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CGullProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase {

        [HttpGet("Item")]
        public async Task<ActionResult> GetAllItems() {
            return BadRequest();    // TODO
        }

        [HttpPost("Item")]
        public async Task<ActionResult> AddItemToCart([Required] Guid cartId, [Required] string itemId, [Required] int quantity) {
            return BadRequest();      // TODO
        }

        [HttpGet("Cart/Details/{id}")]
        public async Task<ActionResult> GetCart([Required] Guid userId) {
            return BadRequest();  // TODO
        }

        [HttpGet("Cart/Total/{id}")]
        public async Task<ActionResult> GetTotals([Required] Guid cartId) {
            return BadRequest();  // TODO
        }

        [HttpPost("ProcessPayment")]
        public async Task<ActionResult> ProcessPayment([Required] Guid cartId, [Required] string cardNumber, [Required] string exp, [Required] string cardholderName, [Required] string cvc) {
            return BadRequest(); // TODO
        }

    }
}