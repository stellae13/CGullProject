using System.ComponentModel.DataAnnotations;
using CGullProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace CGullProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase {

        [HttpGet("Item/GetAllItems")]
        public IActionResult GetAllItems()
        {
            IProductService prodCtx = new ProductService();
            // Calling Product Service's Get Products with null param i.o.2 retrieve all products in DB
            return new JsonResult(prodCtx.GetProductsById(null));
        }

        /// <summary>
        /// Endpoint for request of json array of product instances given string of Guids represented in ASCII and
        /// nondemarkated (i.e: no char separating Guids, since Guids by definition are of fixed len 128B)
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpGet("Item/GetItemsById")]
        public IActionResult GetItemsById(String idList)
        {
            if ((idList.Length & 0x7F) != 0)
                return BadRequest();

            int idCt = idList.Length >> 7;

            String[] ids = new string[idCt];
            IProductService prodCtx = new ProductService();

            for (int i = 0; i < idCt; ++i)
            {
                ids[i] = idList.Substring(i << 7, 128);
            }
            Product[]? productList = prodCtx.GetProductsById(ids);
            if (productList is null)
                return BadRequest();
            return new JsonResult(productList);
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