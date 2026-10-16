using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRojectRentcarwebclident.Models;
using PRojectRentcarwebclident.Services;
namespace PRojectRentcarwebclident.Pages
{
    public class RentModel : PageModel
    {
        private readonly ApiService _apiService;

        public RentModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public CarDto Car { get; set; } = new();
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        [BindProperty]
        public CreateRentalDto RentalData { get; set; } = new();

        public async Task OnGetAsync(string id, DateTime? pickup, DateTime? @return)
        {
            
            if (string.IsNullOrEmpty(id))
            {
                Car = new CarDto { Name = "ID Tidak Ditemukan (Null)" };
                return;
            }

            
            var cars = await _apiService.GetCarsAsync();

            
            var foundCar = cars.FirstOrDefault(c =>
                string.Equals(c.Car_id, id, StringComparison.OrdinalIgnoreCase));

            if (foundCar != null)
            {
                Car = foundCar;
            }
            else
            {
                Car = new CarDto { Name = "Mobil Tidak Ditemukan di Database" };
            }

            PickupDate = pickup ?? DateTime.Today;
            ReturnDate = @return ?? DateTime.Today.AddDays(1);
        }

        public async Task<IActionResult> OnPostConfirmAsync(string Car_id, DateTime Rental_date, DateTime Return_date)
        {
            
            string customerId = _apiService.GetCustomerId();

            
            if (string.IsNullOrEmpty(customerId))
            {
                ModelState.AddModelError(string.Empty, "Sesi habis. Silakan Login ulang.");
                await LoadCarData(Car_id);
                return Page();
            }

            
            var rentalDto = new CreateRentalDto
            {
                Customer_id = customerId,
                Car_id = Car_id,
                Rental_date = Rental_date,
                Return_date = Return_date
            };

            
            var result = await _apiService.CreateRentalAsync(rentalDto);

            
            if (!string.IsNullOrEmpty(result) && result.Contains("berhasil", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToPage("/History");
            }
            else
            {
                
                string errorMessage = !string.IsNullOrEmpty(result) ? result : "Gagal menyewa mobil.";

                ModelState.AddModelError(string.Empty, errorMessage);
                await LoadCarData(Car_id); 
                return Page();
            }
        }


        private async Task LoadCarData(string carId)
        {
            var cars = await _apiService.GetCarsAsync();
            Car = cars.FirstOrDefault(c => c.Car_id == carId) ?? new CarDto();
            PickupDate = RentalData.Rental_date == default ? DateTime.Today : RentalData.Rental_date;
            ReturnDate = RentalData.Return_date == default ? DateTime.Today.AddDays(1) : RentalData.Return_date;
        }


    }
}