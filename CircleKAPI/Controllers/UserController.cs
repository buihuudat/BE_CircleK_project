using CircleKAPI.Data;
using CircleKAPI.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CircleKAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly DataContext dataContext;
        public UserController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        // get all users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await dataContext.Users.ToListAsync();
            return Ok(users);
        }

        // get user by id
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await dataContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        // create user
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser (CreateUser createUser)
        {
            var checkUser = dataContext.Users.Where(u => u.Username == createUser.Username).FirstOrDefault();
            if (checkUser != null)
            {
                return BadRequest("Username has been already");
            }

            CreatePassworHash(createUser.Password, out byte[] hash, out byte[] salt);

            var user = new User
            {
                Id = new Guid(),
                Phone = createUser.Phone,
                Username = createUser.Username,
                Password = hash,
                Salt = salt,
                Permission = createUser.Permission,
                Address = createUser.Address
            };
            await dataContext.Users.AddAsync(user);
            await dataContext.SaveChangesAsync();
            return Ok(user);
        }

        // update user
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UpdateUser updateUser)
        {
            var user = await dataContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            CreatePassworHash(updateUser.Password, out byte[] hash, out byte[] salt);

            user.Name = updateUser.Name;
            user.Avatar = updateUser.Avatar;
            user.Permission = updateUser.Permission;
            user.Phone = updateUser.Phone;
            user.Password = hash;
            user.Salt = salt;

            await dataContext.SaveChangesAsync();
            return Ok(user);
        }

        // delete user
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await dataContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            };

            dataContext.Users.Remove(user);
            await dataContext.SaveChangesAsync();
            return NoContent();
        }

        private void CreatePassworHash(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
