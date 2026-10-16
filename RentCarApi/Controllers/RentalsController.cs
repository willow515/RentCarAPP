namespace RentCarApi.Controllers
{
    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using RentCarApi.Data;
    using RentCarApi.Models;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class RentalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RentalsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        public async Task<ActionResult> CreateRental([FromBody] CreateRentalDto dto)
        {
            
            var customerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

           
            if (string.IsNullOrEmpty(customerId))
            {
                customerId = dto.Customer_id;
            }

         
            if (string.IsNullOrEmpty(customerId))
            {
                return Unauthorized("User ID tidak ditemukan. Pastikan Anda sudah login.");
            }
            var car = await _context.MsCars.FindAsync(dto.Car_id);
            if (car == null || car.Status != 1) return BadRequest("Mobil tidak tersedia");

            var days = (dto.Return_date - dto.Rental_date).Days;
            if (days < 1) days = 1; 

            var totalPrice = days * car.Price_per_day;

            var rental = new TrRental
            {
                Rental_id = Guid.NewGuid().ToString(),
                Rental_date = dto.Rental_date,
                Return_date = dto.Return_date,
                Total_price = totalPrice,
                Payment_status = 0,
                Customer_id = customerId, 
                Car_id = dto.Car_id
            };

            
            car.Status = 0;

            _context.TrRentals.Add(rental);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Sewa berhasil", rental.Rental_id, totalPrice });
        }

        
        [HttpGet("history")]
        public async Task<ActionResult> GetHistory()
        {
            var customerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var history = await _context.TrRentals
                .Where(r => r.Customer_id == customerId)
                .Select(r => new
                {
                    r.Rental_id,
                    r.Rental_date,
                    r.Return_date,
                    r.Total_price,
                    Payment_status = r.Payment_status == 1 ? "Sudah Dibayar" : "Belum Dibayar",
                    Car = _context.MsCars
                        .Where(c => c.Car_id == r.Car_id)
                        .Select(c => new { c.Name, c.Year })
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Ok(history);
        }
    }

    public class CreateRentalDto
    {
        public string Customer_id { get; set; } = string.Empty;
        public string Car_id { get; set; } = string.Empty;
        public DateTime Rental_date { get; set; }
        public DateTime Return_date { get; set; }
    }
}
