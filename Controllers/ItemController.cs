using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Models.DTO;
using CGullProject.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;

namespace CGullProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase {

        private readonly IProductService _productService;
        private readonly IReviewService _reviewService;

        public ItemController(IProductService productService, IReviewService reviewService)
        {
            _productService = productService;
            _reviewService = reviewService;
        }

        [HttpGet("GetAllItems")]
        public async Task<ActionResult> GetAllItems()
        {
           var inventory = await _productService.GetAllProducts();

            return Ok(inventory);
        }
        
        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(String idList)
        {
            var products = await _productService.GetProductsById(idList);

            if (products.IsNullOrEmpty()) {
                return NotFound();
            }

            return Ok(products);
        }

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


        [HttpGet("Image/{id}")]
        public IActionResult GetProductImage(String id)
        {
            if (id[0] == '1')
                return BadRequest($"Given ID {id} belongs to a Bundle, which do not have accompanying photo.");
            try
            {
                var img = System.IO.File.OpenRead($"./Image/{id}.jpg");
                return File(img, "image/jpeg");
            }
            catch (FileNotFoundException e)
            {
                return NotFound($"Item with ID {id} not found in database or doesn't have accompanying photo.");
            }
            
        }

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

        [HttpGet("Category")]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _productService.GetAllCategories();

            return Ok(categories);
        }

        [HttpGet("{id}/Review")]
        public async Task<ActionResult> GetReviews(String id)
        {
            return Ok(await _reviewService.GetReviewsById(id));
        }

        [HttpPost("{id}/Review")]

        public async Task<ActionResult> CreateReview(CreateReviewDTO review,String id)
        {
            if(await _reviewService.AddReview(review, id))
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}