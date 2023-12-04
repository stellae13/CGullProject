using CGullProject.Models.DTO;
using CGullProject.Services;
using CGullProject.Services.ServiceInterfaces;
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
        /// Returns all Items present in the database.
        /// </summary>
        /// <returns><see cref="IEnumerable{Item}"/></returns>
        [HttpGet("GetInventory")]
        public async Task<ActionResult> GetAllItems()
        {
            var inventory = await _service.GetInventory();

            return Ok(inventory);
        }

        /// <summary>
        /// Update the stock of an Item
        /// </summary>
        /// <param name="itemId">Id of Item</param>
        /// <param name="quantity">Quantity to set the stock to</param>
        /// <returns>Success/Failure</returns>
        [HttpPut("UpdateStock")]
        public async Task<ActionResult<bool>> UpdateStock(string itemId, int quantity) {
            return Ok(await _service.UpdateStock(itemId, quantity));
        }

        /// <summary>
        /// Increment or decrement the stock
        /// </summary>
        /// <param name="itemId">Id of Item</param>
        /// <param name="amount">Amount to increase(+) or decrease(-) the stock by</param>
        /// <returns>Success/Failure</returns>
        [HttpPut("AdjustStock")]
        public async Task<ActionResult<bool>> AdjustStock(string itemId, int amount) {
            return Ok(await _service.AdjustStock(itemId, amount));
        }

        /// <summary>
        /// Change the price of an Item
        /// </summary>
        /// <param name="itemId">Id of Item</param>
        /// <param name="price">New price</param>
        /// <returns>Success/Failure</returns>
        [HttpPost("ChangePrice")]
        public async Task<ActionResult<bool>> ChangePrice(string itemId, decimal price) {
            return Ok(await _service.ChangePrice(itemId, price));
        }

        /// <summary>
        /// Add a new Item to the inventory
        /// </summary>
        /// <param name="item">New item</param>
        /// <returns>Success/Failure</returns>
        [HttpPost("AddNewItem")]
        public async Task<ActionResult<bool>> AddNewItem(ItemDTO item) {
            return Ok(await _service.AddNewItem(item));
        }

        [HttpGet("GetAllSalesItems")]
        public async Task<ActionResult> GetAllSalesItems()
        {
            return Ok(await _service.GetAllSalesItems());
        }

        [HttpPut("ChangeSaleStatus")]
        public async Task<ActionResult<bool>> ChangeSaleStus(string itemId, bool status)
        {
            return Ok(await _service.ChangeSalesStatus(itemId, status));
        }
 
    }

}