using CGullProject.Data;
using CGullProject.Models;
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

        public CartController(ShopContext context)
        {
            _context = context;
        }

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
    }
}
