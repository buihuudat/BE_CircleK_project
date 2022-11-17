using CircleKAPI.Data;
using CircleKAPI.Models.Producer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CircleKAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : Controller
    {
        private readonly DataContext dataContext;

        public ProducerController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        // get all producers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producer>>> GetProducers ()
        {
            return await dataContext.Producers.ToListAsync();
        }

        // get producer
        [HttpGet("{id}")]
        public async Task<ActionResult<Producer>> GetProducer (int id)
        {
            var producer = await dataContext.Producers.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }

            return producer;
        }

        // create producer
        [HttpPost]
        public async Task<ActionResult<Producer>> CreateProducer(Producer producer)
        {
            var checkProducer = await dataContext.Producers.FindAsync(producer.Id);
            if (checkProducer != null)
            {
                return BadRequest("Producer has been already");
            }

            await dataContext.Producers.AddAsync(producer);
            await dataContext.SaveChangesAsync();
            return producer;
        }

        // update producer
        [HttpPut("{id}")]
        public async Task<ActionResult<Producer>> UpdateProducer (int id, Producer producer)
        {
            if (id != producer.Id)
            {
                return BadRequest();
            }

            dataContext.Entry(producer).State = EntityState.Modified;

            try
            {
                await dataContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!ProducerExsist(id))
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

        // delete producer
        [HttpDelete("{id}")]
        public async Task<ActionResult<Producer>> DeleteProducer (int id)
        {
            var producer = await dataContext.Producers.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }

            dataContext.Producers.Remove(producer);
            await dataContext.SaveChangesAsync();
            return NoContent();
        }
        private bool ProducerExsist(int id)
        {
            return dataContext.Producers.Any(e => e.Id == id);
        }
    }
}
