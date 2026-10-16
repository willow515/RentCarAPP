using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Data;
using RentCarApi.Models;
namespace RentCarApi.Controllers
{
    
    

    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetCars(int? tahun = null)
        {
            var query = _context.MsCars.AsQueryable();

            if (tahun.HasValue)
            {
                query = query.Where(c => c.Year == tahun.Value);
            }

            var cars = await query
                .Select(c => new
                {
                    c.Car_id,
                    c.Name,
                    c.Model,
                    c.Year,
                    c.Transmission,
                    c.Number_of_car_seats,
                    c.Price_per_day,
                    c.Status,
                    Image_link = _context.MsCarImages
                        .Where(img => img.Car_id == c.Car_id)
                        .Select(img => img.Image_link)
                        .FirstOrDefault() ?? ""
                })
                .ToListAsync();

            return Ok(cars);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetCar(string id)
        {
            var car = await _context.MsCars
                .Where(c => c.Car_id == id)
                .Select(c => new
                {
                    c.Car_id,
                    c.Name,
                    c.Model,
                    c.Year,
                    c.Transmission,
                    c.Number_of_car_seats,
                    c.Price_per_day,
                    c.Status,
                    Image_link = _context.MsCarImages
                        .Where(img => img.Car_id == c.Car_id)
                        .Select(img => img.Image_link)
                        .FirstOrDefault() ?? ""
                })
                .FirstOrDefaultAsync();

            if (car == null) return NotFound();

            return Ok(car);
        }
    }
}
