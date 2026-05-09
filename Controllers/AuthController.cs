using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FashionShopAPI.Models.DTOs;
using FashionShopAPI.Models.Entities;
using FashionShopAPI.Repositories.Interfaces;

namespace FashionShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;

        public AuthController(IUserRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepo.AuthenticateAsync(request.Email, request.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Email or password is incorrect!" });
            }
            var token = GenerateJwtToken(user);

            return Ok(new
            {
                message = "Login successful!",
                token = token,
                user = new { user.UserId, user.FullName, user.Email, user.Role }
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var newUser = new User
            {
                UserId = Guid.NewGuid().ToString(),
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password, 
                Phone = request.Phone,
                Address = request.Address,
                Role = "customer",
                Status = true
            };

            var success = await _userRepo.RegisterAsync(newUser);
            if (!success) return BadRequest(new { message = "Email already exists!" });

            return Ok(new { message = "Registration successful!" });
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7), 
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
