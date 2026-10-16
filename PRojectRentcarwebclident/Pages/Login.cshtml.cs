using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRojectRentcarwebclident.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
namespace PRojectRentcarwebclident.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApiService _apiService;

        public LoginModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public LoginDto Input { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            
            string? token = await _apiService.LoginAsync(Input);

            if (!string.IsNullOrEmpty(token))
            {
                HttpContext.Session.SetString("JwtToken", token);
                string displayName = _apiService.GetCustomerName() ?? Input.Email;
                
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, displayName),
                    new Claim(ClaimTypes.Name, Input.Email), 
                    new Claim("JwtToken", token) 
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                };

                
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Email atau password salah");
            return Page();
        }
    }
}

