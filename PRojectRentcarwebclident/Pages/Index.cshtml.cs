using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRojectRentcarwebclident.Models;
using PRojectRentcarwebclident.Services;
namespace PRojectRentcarwebclident.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<CarDto> Cars { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public DateTime? PickupDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? ReturnDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? FilterTahun { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 4; 

        public async Task OnGetAsync()
        {
            
            var allCars = await _apiService.GetCarsAsync(FilterTahun);

            
            TotalPages = (int)Math.Ceiling(allCars.Count / (double)PageSize);

            
            Cars = allCars
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
} 
