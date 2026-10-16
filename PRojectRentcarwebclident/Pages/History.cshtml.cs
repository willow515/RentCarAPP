using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRojectRentcarwebclident.Models;
using PRojectRentcarwebclident.Services;
namespace PRojectRentcarwebclident.Pages
{
    public class HistoryModel : PageModel
    {
        private readonly ApiService _apiService;

        public HistoryModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<RentalHistoryDto> History { get; set; } = new();

        public async Task OnGetAsync()
        {
            History = await _apiService.GetHistoryAsync();
        }
    }

    
}