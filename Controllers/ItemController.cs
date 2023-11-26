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
        /// IProductService that the controller will use to perform Product related data operations
        /// </summary>
        private readonly IProductService _productService;
        /// <summary>
        /// IReviewService that the controller will use to perform Review related data operations
        /// </summary>
        private readonly IReviewService _reviewService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productService">Target IProductService</param>
        /// <param name="reviewService">Target IReviewService</param>
        public ItemController(IProductService productService, IReviewService reviewService)
        {
            _productService = productService;
            _reviewService = reviewService;
        }

        /// <summary>
        /// Returns all Products present in the database.
        /// </summary>
        /// <returns>IEnumerable&lt;Product&gt;</returns>
        [HttpGet("GetAllItems")]
        public async Task<ActionResult> GetAllItems()
        {
           var inventory = await _productService.GetAllProducts();

            return Ok(inventory);
        }
        
        /// <summary>
        /// Get a List of Products based on a delimited Id string
        /// </summary>
        /// <param name="idList">String of delimited Ids</param>
        /// <returns>IEnumerable&lt;Product&gt;</returns>
        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(String idList)
        {
            var products = await _productService.GetProductsById(idList);

            if (products.IsNullOrEmpty()) {
                return NotFound();
            }

            return Ok(products);
        }

        /// <summary>
        /// Add a Product to a Cart
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
                await _productService.AddItemToCart(cartId, itemId, quantity);
                return Ok($"Product with ID {itemId} added to cart with ID {cartId}.");
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
        /// Get a list of relevant Products that match a string of keywords.
        /// </summary>
        /// <param name="keywordList">String keywords</param>
        /// <returns>IEnumerable&lt;Product&gt;</returns>
        [HttpGet("GetByKeyword")]
        public async Task<ActionResult> GetByKeyword(String keywordList)
        {
            var products = await _productService.GetProductsByKeyword(keywordList);
            if (products.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(products);
        }

        /// <summary>
        /// Get the contents of a Bundle
        /// </summary>
        /// <param name="bundleId">String bundleId</param>
        /// <returns>IEnumerable&lt;Product&gt;</returns>
        [HttpGet("GetBundleProducts")]
        public async Task<ActionResult> GetBundleProducts(String bundleId) 
        {
            try
            {
                IEnumerable<Product> bundleContents = 
                    await _productService.GetBundledProducts(bundleId);
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
        /// Get the image associated with a Product
        /// </summary>
        /// <param name="id">String productId</param>
        /// <returns>File</returns>
        [HttpGet("Image/{id}")]
        public ActionResult GetProductImage(String id)
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
        /// Get all Products that belong to a Category
        /// </summary>
        /// <param name="id">int id of the Category</param>
        /// <returns>IEnumerable&lt;Product&gt;</returns>
        [HttpGet("Category/{id}")]
        public async Task<ActionResult> GetProductByCategory(int id)
        {
            var products = await _productService.GetProductsbyCategory(id);

            if (products.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(products);
        }

        /// <summary>
        /// Get a list of all Categories
        /// </summary>
        /// <returns>IEnumerable&lt;Category&gt;</returns>
        [HttpGet("Category")]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _productService.GetAllCategories();

            return Ok(categories);
        }

        /// <summary>
        /// Get a list of all reviews for a specific product
        /// </summary>
        /// <param name="id">String id of Product</param>
        /// <returns>IEnumerable&lt;Review&gt;</returns>
        [HttpGet("{id}/Review")]
        public async Task<ActionResult> GetReviews(String id)
        {
            return Ok(await _reviewService.GetReviewsById(id));
        }

        /// <summary>
        /// Create a new Review for a Product
        /// </summary>
        /// <param name="review">CreateReviewDTO review</param>
        /// <param name="id">String id of Product</param>
        /// <returns>ActionResult</returns>
        [HttpPost("{id}/Review")]
        public async Task<ActionResult> CreateReview(CreateReviewDTO review,String id)
        {
            if(await _reviewService.AddReview(review, id))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("ChangeSaleStatus")]
        public async Task<ActionResult<bool>> ChangeSaleStus(string itemId, bool status)
        {
            return Ok(await _productService.ChangeSalesStatus(itemId, status));
        }

        [HttpGet("GetAllSalesProducts")]
        public async Task<ActionResult> GetAllSalesProducts()
        {
            return Ok(await _productService.GetAllSalesProducts());
        }



    }
}