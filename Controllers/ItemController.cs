using CGullProject.Models;
using CGullProject.Models.DTO;
using CGullProject.Services;
using CGullProject.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CGullProject.Controllers
{
    /// <summary>
    /// Stores the Item endpoints
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase {

        /// <summary>
        /// IItemService that the controller will use to perform Item related data operations
        /// </summary>
        private readonly IItemService _itemService;
        /// <summary>
        /// IReviewService that the controller will use to perform Review related data operations
        /// </summary>
        private readonly IReviewService _reviewService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemService">Target IItemService</param>
        /// <param name="reviewService">Target IReviewService</param>
        public ItemController(IItemService itemService, IReviewService reviewService)
        {
            _itemService = itemService;
            _reviewService = reviewService;
        }


        /// <summary>
        /// Get a List of Items based on a delimited Id string
        /// </summary>
        /// <param name="idList">String of delimited Ids</param>
        /// <returns><see cref="IEnumerable{Item}"/></returns>
        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(String idList)
        {
            var items = await _itemService.GetItemsById(idList);

            if (items.IsNullOrEmpty()) {
                return NotFound();
            }

            return Ok(items);
        }

        /// <summary>
        /// Add an Item to a Cart
        /// </summary>
        /// <param name="cartId">Guid cartId of the cart to be added to</param>
        /// <param name="itemId">String id of the item</param>
        /// <param name="quantity">int quantity of items to add to cart</param>
        /// <returns>ActionResult</returns>
        [HttpPost("AddItemToCart")]
        public async Task<ActionResult> AddItemToCart(Guid cartId, string itemId, int quantity)
        {
            try
            {
                await _itemService.AddItemToCart(cartId, itemId, quantity);
                return Ok($"Item with ID {itemId} added to cart with ID {cartId}.");
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }

        }

        /// <summary>
        /// Remove an item from a Cart
        /// </summary>
        /// <param name="cartId">Id of the Cart</param>
        /// <param name="itemId">Id of the item</param>
        /// <returns>Success/Failure</returns>
        [HttpDelete("RemoveFromCart")]
        public async Task<ActionResult> RemoveFromCart(Guid cartId, string itemId) {
            return Ok(await _itemService.RemoveItemFromCart(cartId, itemId));
        }

        /// <summary>
        /// Get a list of relevant Items that match a string of keywords.
        /// </summary>
        /// <param name="keywordList">String keywords</param>
        /// <returns><see cref="IEnumerable{Item}"/></returns>
        [HttpGet("GetByKeyword")]
        public async Task<ActionResult> GetByKeyword(String keywordList)
        {
            var items = await _itemService.GetItemsByKeyword(keywordList);
            if (items.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(items);
        }

        /// <summary>
        /// Get the contents of a Bundle
        /// </summary>
        /// <param name="bundleId">String bundleId</param>
        /// <returns><see cref="IEnumerable{Item}"/></returns>
        [HttpGet("GetBundleItems")]
        public async Task<ActionResult> GetBundleItems(String bundleId) 
        {
            try
            {
                IEnumerable<Item> bundleContents = 
                    await _itemService.GetBundledItems(bundleId);
                return Ok(bundleContents);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Get the image associated with an Item
        /// </summary>
        /// <param name="id">String itemId</param>
        /// <returns>File</returns>
        [HttpGet("Image/{id}")]
        public ActionResult GetItemImage(String id)
        {
            try
            {
                var img = System.IO.File.OpenRead($"./Images/{id}.jpg");
                return File(img, "image/jpeg");
            } catch (FileNotFoundException e)
            {
                return NotFound($"No image found matching Item with ID, {id}");
            }
            
        }

        /* Commented out until cycle issue with Bundle.BundleItem gets
         [HttpGet("GetAssociatedBundles")]
        public async Task<ActionResult> GetAssociatedBundles(String id)
        {
            try
            {
                IEnumerable<Bundle> bundles = await _productService.GetAssociatedBundles(id);
                if (bundles.IsNullOrEmpty())
                    return NotFound($"Item with ID {id} either DNE or has no associated bundles.");
                return Ok(bundles);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            
        }*/

        /// <summary>
        /// Get all Items that belong to a Category
        /// </summary>
        /// <param name="id">int id of the Category</param>
        /// <returns><see cref="IEnumerable{Item}"/></returns>
        [HttpGet("Category/{id}")]
        public async Task<ActionResult> GetItemByCategory(int id)
        {
            var items = await _itemService.GetItemsByCategory(id);

            if (items.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(items);
        }

        /// <summary>
        /// Get a list of all Categories
        /// </summary>
        /// <returns>IEnumerable&lt;Category&gt;</returns>
        [HttpGet("Category")]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _itemService.GetAllCategories();

            return Ok(categories);
        }

        /// <summary>
        /// Get a list of all reviews for a specific Item
        /// </summary>
        /// <param name="id">String id of Item</param>
        /// <returns>IEnumerable&lt;Review&gt;</returns>
        [HttpGet("{id}/Review")]
        public async Task<ActionResult> GetReviews(String id)
        {
            return Ok(await _reviewService.GetReviewsById(id));
        }

        /// <summary>
        /// Create a new Review for an Item
        /// </summary>
        /// <param name="review">CreateReviewDTO review</param>
        /// <param name="id">String id of Item</param>
        /// <returns>ActionResult</returns>
        [HttpPost("{id}/Review")]
        public async Task<ActionResult> CreateReview(CreateReviewDTO review, String id)
        {
            if(await _reviewService.AddReview(review, id))
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}