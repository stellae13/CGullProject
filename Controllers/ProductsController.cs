using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CGullProject;
using CGullProject.Data;

namespace CGullProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;
        }

        // Unneccesary code - rml
        //// GET: api/Products
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        //{
        //    return await _context.Product.Include(x => x.Category).ToListAsync();
        //}

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(Product product)
        {
            var dbProduct = await _context.Product.FindAsync(product.Id);

            if (dbProduct == null)
            {
                return NotFound($"Product with ID {product.Id} not found.");
            }

            dbProduct.Name = product.Name;
            dbProduct.CategoryId = product.CategoryId;
            dbProduct.Price = product.Price;
            dbProduct.Rating = product.Rating;
            dbProduct.Stock = product.Stock;

            await _context.SaveChangesAsync();

            return Ok($"Product with ID {product.Id} updated.");
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product with ID {id} not found.");
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return Ok($"Deleted product with ID {id}.");
        }
    }
}
