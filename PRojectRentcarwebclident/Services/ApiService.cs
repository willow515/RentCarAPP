using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using PRojectRentcarwebclident.Models;
namespace PRojectRentcarwebclident.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _token = string.Empty;

        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri("http://localhost:5216/"); 
            LoadToken();
        }

        private void LoadToken()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
            if (!string.IsNullOrEmpty(token))
            {
                _token = token;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            }
        }

        private void SaveSession(string token, string customerId,string name)
        {
            _token = token;
           
            _httpContextAccessor.HttpContext?.Session.SetString("JwtToken", token);
            _httpContextAccessor.HttpContext?.Session.SetString("CustomerName", name);
            
            if (!string.IsNullOrEmpty(customerId))
            {
                _httpContextAccessor.HttpContext?.Session.SetString("CustomerId", customerId);
                
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public string GetCustomerId()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("CustomerId") ?? string.Empty;
        }
        public string? GetCustomerName()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("CustomerName");
        }

        public void ClearToken()
        {
            _token = string.Empty;
            _httpContextAccessor.HttpContext?.Session.Remove("JwtToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        
        public async Task<List<CarDto>> GetCarsAsync(int? year = null)
        {
            var url = "api/cars";
            if (year.HasValue) url += $"?tahun={year}";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<CarDto>>(json, options) ?? new List<CarDto>();
            }
            return new List<CarDto>();
        }

    
        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/auth/register", content);
            return await response.Content.ReadAsStringAsync();
        }

        
        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<LoginResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result != null)
                {
                    
                    SaveSession(result.Token, result.Customer_id,result.Name);
                    return result.Token;
                }
            }
            return null;
        }

       
        public async Task<string> CreateRentalAsync(CreateRentalDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/rentals", content);
            return await response.Content.ReadAsStringAsync();
        }

        
        public async Task<List<RentalHistoryDto>> GetHistoryAsync()
        {
            var response = await _httpClient.GetAsync("api/rentals/history");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<RentalHistoryDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
            return new();
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

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Customer_id { get; set; } = string.Empty;
    }

    public class CreateRentalDto
    {
        public string Customer_id { get; set; } = string.Empty;
        public string Car_id { get; set; } = string.Empty;
        public DateTime Rental_date { get; set; }
        public DateTime Return_date { get; set; }
    }

    public class RentalHistoryDto
    {
        public string Rental_id { get; set; } = string.Empty;
        public DateTime Rental_date { get; set; }
        public DateTime Return_date { get; set; }
        public decimal Total_price { get; set; }
        public string Payment_status { get; set; } = string.Empty;
        public CarInfoDto? Car { get; set; }
    }

    public class CarInfoDto
    {
        public string Name { get; set; } = string.Empty;
        public int Year { get; set; }
    }
}
