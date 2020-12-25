using CatalogService.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CatalogController(DatabaseContext context)
        {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>>
            GetProducts()
        {

            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {

                return NotFound();
            }

            return product;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {

            if (id != product.ProductId)
            {
                return BadRequest();


            }
            _context.Entry(product).State = EntityState.Modified;
            try
            {

                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {

                if (!ProductExists(id))
                {

                    return NotFound();

                }
                else
                {

                    throw;
                }


            }

            return NoContent();
        }

        [HttpPost]

        public async Task<ActionResult<Product>> PostProduct(Product product)
        {

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {


            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {

                return NotFound();

            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);

        }
        // GET: api/<CatalogController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<CatalogController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<CatalogController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<CatalogController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CatalogController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
