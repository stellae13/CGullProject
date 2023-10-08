using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CGullProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase {

        [HttpGet("Item")]
        public async Task<ActionResult> GetAllItems() {
            return NotFound();    // TODO
        }

        [HttpPost("Item")]
        public async Task<ActionResult> AddItemToCart([Required] Guid cartId, [Required] string itemId, [Required] int quantity) {
            return NotFound();      // TODO
        }

        [HttpGet("Cart/Details/{id}")]
        public async Task<ActionResult> GetCart([Required] Guid userId) {
            return NotFound();  // TODO
        }

        [HttpGet("Cart/Total/{id}")]
        public async Task<ActionResult> GetTotals([Required] Guid cartId) {
            return NotFound();  // TODO
        }

    }
}