using CGullProject.Models;
using CGullProject.Models.DTO;
using CGullProject.Services;
using CGullProject.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CGullProject.Controllers
{
    /// <summary>
    /// Stores the <see cref="Item"/> endpoints
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase {

        /// <summary>
        /// Service that the controller will use to perform <see cref="Item"/> related data operations
        /// </summary>
        private readonly IItemService _itemService;
        /// <summary>
        /// Service that the controller will use to perform <see cref="Review"/> related data operations
        /// </summary>
        private readonly IReviewService _reviewService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemService">Target <see cref="IItemService"/> </param>
        /// <param name="reviewService">Target <see cref="IReviewService"/> </param>
        public ItemController(IItemService itemService, IReviewService reviewService)
        {
            _itemService = itemService;
            _reviewService = reviewService;
        }

        /// <summary>
        /// Get a List of Items based on a delimited Id string
        /// </summary>
        /// <param name="idList">String of multipe Ids delimited with '&'</param>
        /// <returns><see cref="Item"/>s matching any Id in the idList</returns>
        [HttpGet("GetById")]
        public async Task<ActionResult<IEnumerable<Item>>> GetById(string idList)
        {
            var items = await _itemService.GetItemsById(idList);

            if (items.IsNullOrEmpty()) {
                return NotFound();
            }

            return Ok(items);
        }

        /// <summary>
        /// Get a list of relevant <see cref="Item"/>s  that match a string of keywords.
        /// </summary>
        /// <param name="keywordList">String of keywords delimited by '&'</param>
        /// <returns>Items that match the given keywords</returns>
        [HttpGet("GetByKeyword")]
        public async Task<ActionResult> GetByKeyword(string keywordList)
        {
            var items = await _itemService.GetItemsByKeyword(keywordList);
            if (items.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(items);
        }

        /// <summary>
        /// Get the contents of a <see cref="Bundle"/> 
        /// </summary>
        /// <param name="bundleId">Id of the <see cref="Bundle"/></param>
        /// <returns><see cref="Item"/>s in a <see cref="Bundle"/></returns>
        [HttpGet("GetBundleItems")]
        public async Task<ActionResult<IEnumerable<Item>>> GetBundleItems(String bundleId) 
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
        /// Get the image associated with an <see cref="Item"/> 
        /// </summary>
        /// <param name="id">Id of the <see cref="Item"/></param>
        /// <returns>The image associated with the <see cref="Item"/></returns>
        [HttpGet("Image/{id}")]
        public ActionResult GetItemImage(string id)
        {
            try
            {
                var img = System.IO.File.OpenRead($"./Images/{id}.jpg");
                return File(img, "image/jpeg");
            } catch (FileNotFoundException e)
            {
                return NotFound($"No image found matching Item with ID, {id}.\n{e}");
            }
            
        }

        /// <summary>
        /// Get all <see cref="Item"/>s that belong to a <see cref="Category"/> 
        /// </summary>
        /// <param name="id">Id of the <see cref="Category"/></param>
        /// <returns><see cref="Item"/>s that belong to a <see cref="Category"/> </returns>
        [HttpGet("Category/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemByCategory(int id)
        {
            var items = await _itemService.GetItemsByCategory(id);

            if (items.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(items);
        }

        /// <summary>
        /// Get a list of all <see cref="Category"/>s
        /// </summary>
        /// <returns>List of <see cref="Category"/>s</returns>
        [HttpGet("Category")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _itemService.GetAllCategories();

            return Ok(categories);
        }

        /// <summary>
        /// Get a list of all <see cref="Review"/>s for a specific <see cref="Item"/> 
        /// </summary>
        /// <param name="id">Id of <see cref="Item"/> </param>
        /// <returns>List of reviews</returns>
        [HttpGet("{id}/Review")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews(string id)
        {
            return Ok(await _reviewService.GetReviewsById(id));
        }

        /// <summary>
        /// Create a new <see cref="Review"/> for an <see cref="Item"/> 
        /// </summary>
        /// <param name="review">review</param>
        /// <param name="id">Id of <see cref="Item"/></param>
        /// <returns>Success/Failure</returns>
        [HttpPost("{id}/Review")]
        public async Task<ActionResult<bool>> CreateReview(CreateReviewDTO review, string id)
        {
            if(await _reviewService.AddReview(review, id))
            {
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Get all Bundles associated with a Product
        /// </summary>
        /// <param name="id">String Id of the Product</param>
        /// <returns>IEnumerable&lt;Bundle&gt;</returns>
        [HttpGet("GetAssociatedBundles")]
        public async Task<ActionResult> GetAssociatedBundles(string id)
        {
            try
            {
                IEnumerable<Bundle> bundles = await _itemService.GetAssociatedBundle(id);
                if (bundles.IsNullOrEmpty())
                    return NotFound($"Item with ID {id} either DNE or has no associated bundles.");
                return Ok(bundles);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}