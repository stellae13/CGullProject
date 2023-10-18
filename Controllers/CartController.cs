using CGullProject.Data;
using CGullProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CGullProject.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<List<CartDetail>>> GetCart([Required] int cartId)
        {
            return Ok(await _context.CartDetails.FromSqlInterpolated($"exec usp_GetCartDetails {cartId}").ToListAsync());
        }

        // return the different totals for the cart
        [HttpGet("GetTotals")]
        public async Task<ActionResult<List<CartTotals>>> GetTotals([Required] int cartId)
        {
            return Ok(await _context.CartTotals.FromSqlInterpolated($"exec usp_GetCartTotals {cartId}").ToListAsync());
        }
    }
}
