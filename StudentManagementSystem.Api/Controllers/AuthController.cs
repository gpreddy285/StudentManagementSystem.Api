using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public IActionResult Register(string username, string password)
        {
            var user = new IdentityUser
            {
                UserName = username
            };

            var result = _userManager.CreateAsync(user, password).Result;

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User registered successfully");
        }
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var user = _userManager.FindByNameAsync(username).Result;

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var result = _userManager.CheckPasswordAsync(user, password).Result;

            if (!result)
            {
                return Unauthorized("Invalid username or password");
            }

            var claims = new[]
            {
               new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("ThisIsMySecretKeyForStudentManagementSystem123"));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jwt);
        }
    }
}
