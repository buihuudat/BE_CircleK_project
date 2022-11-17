using CircleKAPI.Data;
using CircleKAPI.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CircleKAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly DataContext dataContext;

        public ProductController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        // get all products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await dataContext.Products.ToListAsync();
        }

        // get product
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await dataContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // create product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            dataContext.Products.Add(product);
            await dataContext.SaveChangesAsync();
            return product;
        }

        // update product
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            dataContext.Entry(product).State = EntityState.Modified;

            try
            {
                await dataContext.SaveChangesAsync();
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

        // delete product
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeletePeoduct (int id)
        {
            var product = await dataContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            dataContext.Products.Remove(product);
            await dataContext.SaveChangesAsync();
            return NoContent();
        }


        private bool ProductExists(int id)
        {
            return dataContext.Products.All(x => x.Id == id);
        }

    }
}
