using System.ComponentModel.DataAnnotations;
using CGullProject.Data;
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