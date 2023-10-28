using System.ComponentModel.DataAnnotations;
using CGullProject.Data;
using CGullProject.Services;
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

        public ItemController(IProductService productService)
        {
            _productService = productService;
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

        [HttpGet("GetProductByCategory")]
        public async Task<ActionResult> GetProductByCategory(int categoryID)
        {
            var products = await _productService.GetProductsbyCategory(categoryID);

            if (products.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _productService.GetAllCategories();

            return Ok(categories);
        }


    }
}