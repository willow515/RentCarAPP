using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RentCarApi.Data;
using RentCarApi.Models;
namespace RentCarApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.MsCustomers.AnyAsync(c => c.Email == dto.Email))
                return BadRequest("Email sudah terdaftar");

            var customer = new MsCustomer
            {
                Customer_id = Guid.NewGuid().ToString(),
                Email = dto.Email,
                Password = dto.Password, 
                Name = dto.Name,
                Phone_number = dto.Phone_number,
                Address = dto.Address,
                Driver_license_number = dto.Driver_license_number ?? ""
            };

            _context.MsCustomers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registrasi berhasil" });
        }

        
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            var customer = await _context.MsCustomers
                .FirstOrDefaultAsync(c => c.Email == dto.Email && c.Password == dto.Password);

            if (customer == null)
                return Unauthorized("Email atau password salah");

            var token = GenerateJwtToken(customer);

            return Ok(new { token, customer.Name, customer.Customer_id });
        }

        private string GenerateJwtToken(MsCustomer customer)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, customer.Customer_id),
        new Claim(ClaimTypes.Email, customer.Email),
        new Claim(ClaimTypes.Name, customer.Name)
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class RegisterDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone_number { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Driver_license_number { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
