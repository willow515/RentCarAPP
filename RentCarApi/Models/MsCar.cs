namespace RentCarApi.Models
{
    public class MsCar
    {
        public string Car_id { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string License_plate { get; set; } = string.Empty;
        public int Number_of_car_seats { get; set; }
        public string Transmission { get; set; } = string.Empty;
        public decimal Price_per_day { get; set; }
        public int Status { get; set; } 
    }

}
