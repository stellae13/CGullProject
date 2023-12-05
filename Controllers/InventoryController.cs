using CGullProject.Models.DTO;
using CGullProject.Services;
using CGullProject.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

using CGullProject.Models;

namespace CGullProject.Controllers
{
    /// <summary>
    /// Stores the Inventory endpoints
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        /// <summary>
        /// Inventory service
        /// </summary>
        private readonly IInventoryService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">Inventory service</param>
        public InventoryController(IInventoryService service) {
            _service = service;
        }

        /// <summary>
        /// Returns all <see cref="Item" />s in inventory
        /// </summary>
        /// <returns><see cref="IEnumerable{Item}"/></returns>
        [HttpGet("GetInventory")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            var inventory = await _service.GetInventory();

            return Ok(inventory);
        }

        /// <summary>
        /// Update the stock of an <see cref="Item"/> 
        /// </summary>
        /// <param name="itemId">Id of <see cref="Item"/></param>
        /// <param name="quantity">Quantity to set the stock to</param>
        /// <returns>Success/Failure</returns>
        [HttpPut("UpdateStock")]
        public async Task<ActionResult<bool>> UpdateStock(string itemId, int quantity) {
            return Ok(await _service.UpdateStock(itemId, quantity));
        }

        /// <summary>
        /// Increment or decrement the stock
        /// </summary>
        /// <param name="itemId">Id of <see cref="Item"/></param>
        /// <param name="amount">Amount to increase(+) or decrease(-) the stock by</param>
        /// <returns>Success/Failure</returns>
        [HttpPut("AdjustStock")]
        public async Task<ActionResult<bool>> AdjustStock(string itemId, int amount) {
            return Ok(await _service.AdjustStock(itemId, amount));
        }

        /// <summary>
        /// Change the price of an <see cref="Item"/>
        /// </summary>
        /// <param name="itemId">Id of <see cref="Item"/></param>
        /// <param name="price">New price</param>
        /// <returns>Success/Failure</returns>
        [HttpPost("ChangePrice")]
        public async Task<ActionResult<bool>> ChangePrice(string itemId, decimal price) {
            return Ok(await _service.ChangePrice(itemId, price));
        }

        /// <summary>
        /// Add a new <see cref="Item"/> to the inventory
        /// </summary>
        /// <param name="item">New <see cref="Item"/></param>
        /// <returns>Success/Failure</returns>
        [HttpPost("AddNewItem")]
        public async Task<ActionResult<bool>> AddNewItem(ItemDTO item) {
            return Ok(await _service.AddNewItem(item));
        }

        /// <summary>
        /// Get all the <see cref="Item"/>s that are on sale
        /// </summary>
        /// <returns><see cref="IEnumerable{Item}"/> of on-sale items</returns>
        [HttpGet("GetAllSalesItems")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllSalesItems()
        {
            return Ok(await _service.GetAllSalesItems());
        }

        /// <summary>
        /// Change the sale status of an <see cref="Item"/>
        /// </summary>
        /// <param name="itemId">Id of <see cref="Item"/></param>
        /// <param name="status">New sale status</param>
        /// <returns>Success/Failure</returns>
        [HttpPut("ChangeSaleStatus")]
        public async Task<ActionResult<bool>> ChangeSaleStatus(string itemId, bool status)
        {
            return Ok(await _service.ChangeSalesStatus(itemId, status));
        }
 
    }

}