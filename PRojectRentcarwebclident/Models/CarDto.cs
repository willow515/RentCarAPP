namespace PRojectRentcarwebclident.Models
{
    public class CarDto
    {
        public string Car_id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Transmission { get; set; } = string.Empty;
        public int Number_of_car_seats { get; set; }
        public decimal Price_per_day { get; set; }
        public string Image_link { get; set; } = string.Empty;
    }
}