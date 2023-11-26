using CGullProject.Models.DTO;
using CGullProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace CGullProject.Controllers
{
    /// <summary>
    /// Stores the Inventory endpoints
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service) {
            _service = service;
        }

        /// <summary>
        /// Update the stock of a Product
        /// </summary>
        /// <param name="itemId">Id of Product</param>
        /// <param name="quantity">Amount to increase(+) or decrease(-) the stock by</param>
        /// <returns>Success/Failure</returns>
        [HttpPost("UpdateStock")]
        public async Task<ActionResult<bool>> UpdateStock(string itemId, int quantity) {
            return Ok(await _service.UpdateStock(itemId, quantity));
        }

        /// <summary>
        /// Change the price of a Product
        /// </summary>
        /// <param name="itemId">Id of Product</param>
        /// <param name="price">New price</param>
        /// <returns>Success/Failure</returns>
        [HttpPost("ChangePrice")]
        public async Task<ActionResult<bool>> ChangePrice(string itemId, decimal price) {
            return Ok(await _service.ChangePrice(itemId, price));
        }

        /// <summary>
        /// Add a new product to inventory
        /// </summary>
        /// <param name="product">New product</param>
        /// <returns>Success/Failure</returns>
        [HttpPost("AddNewItem")]
        public async Task<ActionResult<bool>> AddNewItem(ProductDTO product) {
            return Ok(await _service.AddNewItem(product));
        }

      

    }

}