using CircleKAPI.Data;
using CircleKAPI.Models.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CircleKAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly DataContext dataContext;

        public OrderController(DataContext dataContext) {
            this.dataContext = dataContext;
        }

        // get all orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await dataContext.Orders.ToListAsync();
        }

        //get order
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await dataContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // get order by UID
        [HttpGet("user/{uid:guid}")]
        public async Task<ActionResult<Order>> GetOrderByUID (Guid uid)
        {
            var user = await dataContext.Users.FindAsync(uid);
            if (user == null)
            {
                return NotFound();
            }
            var order = dataContext.Orders.Where(e => (e.UID == user.Id.ToString())).FirstOrDefault();

            if (order == null)
            {
                return BadRequest();
            }

            return Ok(order);
        }

        // create order
        [HttpPost("create")]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            await dataContext.Orders.AddAsync(order);
            await dataContext.SaveChangesAsync();
            return Ok(order);
        }


        // update order
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Order>> UpdateOrder (int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            dataContext.Entry(order).State = EntityState.Modified;

            try
            {
                await dataContext.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException)
            {
                if (!OrderExsist(id))
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

        // delete order
        [HttpDelete("delete/id")]
        public async Task<ActionResult<Order>> DeleteOrder (int id)
        {
            var order = await dataContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            dataContext.Remove(order);
            await dataContext.SaveChangesAsync();
            return NoContent();
        }

        // delete order by UID
        [HttpDelete("user/{uid:guid}")]
        public async Task<ActionResult<Order>> DeleteOrderByUID(Guid uid)
        {
            var order = await dataContext.Orders.Where(e => e.UID == uid.ToString()).FirstOrDefaultAsync();
            dataContext.Remove(order);
            await dataContext.SaveChangesAsync();
            return NoContent();
        }

        private bool OrderExsist(int id)
        {
            return dataContext.Orders.Any(e => e.Id == id);
        }
    }
}
