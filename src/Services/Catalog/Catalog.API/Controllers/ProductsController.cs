using Catalog.API.Data;
using Catalog.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CatalogDbContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(CatalogDbContext context ,ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // Bacuse: YAGNI - You aren't gonna need it.
        // KISS - Keep It Simple, Stupid
        // I don't need Repository right now, maybe later, but now i don't need it. Same for DTOs. and for Pagination.

        // GET: api/v1/products
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var result = await _context.Products.ToListAsync();

            return Ok(result);
        }

        // GET: api/v1/products/1
        [HttpGet]
        [Route("{id:int}", Name = nameof(GetProductById))]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var result = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return NotFound("Product was not found");
            }

            return Ok(result);
        }

        // POST: api/v1/products
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Product>> CreateProduct(Product productToCreate)
        {
            var item = new Product
            {
                Title = productToCreate.Title,
                Description = productToCreate.Description,
                ProductBrandId = productToCreate.ProductBrandId,
                ProductTypeId = productToCreate.ProductTypeId,
                Price = productToCreate.Price
            };

            _context.Products.Add(item);
            var result = await _context.SaveChangesAsync() > 0;

            if (result == false)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetProductById), new { id = item.Id }, item);
        }

        // PUT: api/v1/products
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Product>> UpdateProduct(Product productToUpdate)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == productToUpdate.Id);

            if (product == null)
            {
                return NotFound();
            }

            // Update current product
            product = productToUpdate;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = productToUpdate.Id }, product);
        }

        // DELETE: api/v1/products/1
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
