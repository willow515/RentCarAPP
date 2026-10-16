namespace RentCarApi.Models
{
    public class TrRental
    {
        public string Rental_id { get; set; } = string.Empty; 
        public DateTime Rental_date { get; set; }
        public DateTime Return_date { get; set; }
        public decimal Total_price { get; set; }
        public int Payment_status { get; set; } 
        public string Customer_id { get; set; } = string.Empty; 
        public string Car_id { get; set; } = string.Empty; 

        
       
    }
}
