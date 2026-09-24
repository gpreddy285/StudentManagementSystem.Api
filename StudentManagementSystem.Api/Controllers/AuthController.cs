using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
            var roles = _userManager.GetRolesAsync(user).Result;

            var claims = new List<Claim>
            {
             new Claim(ClaimTypes.Name, user.UserName)
             };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


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
        [HttpPost("create-roles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public IActionResult CreateRoles()
        {
            string[] roles = { "Admin", "Teacher", "Student" };

            foreach (var role in roles)
            {
                if (!_roleManager.RoleExistsAsync(role).Result)
                {
                    _roleManager.CreateAsync(new IdentityRole(role)).Wait();
                }
            }

            return Ok("Roles created successfully");
        }
        [HttpPost("assign-role")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        public IActionResult AssignRole(string username, string role)
        {
            var user = _userManager.FindByNameAsync(username).Result;

            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = _userManager.AddToRoleAsync(user, role).Result;

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Role assigned successfully");
        }
    }
}
