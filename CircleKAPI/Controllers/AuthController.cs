using CircleKAPI.Data;
using CircleKAPI.Models.Auth;
using CircleKAPI.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CircleKAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly DataContext dataContext;
        private IConfiguration _configuration;

        public AuthController(DataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            _configuration = configuration;
        }


        [HttpPost("Signin")]
        public async Task<IActionResult> Signin (Signin signin)
        {
            var user = dataContext.Users.Where(X => X.Username == signin.Username).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Username not found");
            }
            if (user.Username != signin.Username)
            {
                return BadRequest("Invalid username");
            }
            if (!CheckPassword(signin.Password, user.Password, user.Salt))
            {
                return BadRequest("Password wrong");
            }

            var token = CreateToken(user);
            var data = new { user = user, token = token };
            return Json(data);

        }


        [HttpPost("Signup")]
        public async Task<ActionResult<User>> Signup(Signup signup)
        {
            var checkUser = dataContext.Users.Where(x => x.Username == signup.Username).FirstOrDefault();
            if (checkUser != null)
            {
                return BadRequest("Username has been already");
            }
            CreatePasswordHash(signup.Password, out byte[] hash, out byte[] salt);

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = signup.Name,
                Phone = signup.Phone,
                Username = signup.Username,
                Password = hash,
                Salt = salt
            };

            await dataContext.Users.AddAsync(user);
            await dataContext.SaveChangesAsync();

            var token = CreateToken(user);

            var data = new {user = user, token = token};
            return Json(data);
        }

        private bool CheckPassword(string password, byte[] hash, byte[] salt)
        {
            using(var hmac = new HMACSHA512(salt))
            {
                var passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return passHash.SequenceEqual(hash);
            }
        }

        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
