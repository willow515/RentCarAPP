using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRojectRentcarwebclident.Services;

namespace PRojectRentcarwebclident.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApiService _apiService;

        public RegisterModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public RegisterDto Input { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _apiService.RegisterAsync(Input);
            if (result.Contains("berhasil"))
            {
                return RedirectToPage("/Login");
            }
            ModelState.AddModelError(string.Empty, "Registrasi gagal");
            return Page();
        }
    }
}
