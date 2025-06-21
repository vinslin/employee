using EmployeeAdminPortal.Data.Data;
using EmployeeAdminPortal.Data.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public UserController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        // POST: api/User/register
        [HttpPost("register")]
        public IActionResult Register(RegisterDto request)
        {
            // Check if user already exists
            var exists = _dbContext.Users.Any(u => u.Username == request.Username);
            if (exists) return BadRequest("User already exists");

            // Create new user
            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                Username = request.Username,
                Password = request.Password, // In production, hash passwords!
                Email = request.Email
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return Ok("User registered successfully");
        }

        // POST: api/User/login
        [HttpPost("login")]
        public IActionResult Login(LoginDto request)
        {
            var user = _dbContext.Users
                .FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        // Token Generator
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
