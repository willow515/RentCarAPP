namespace RentCarApi.Models
{
    public class LtPayment
    {
        public string Payment_id { get; set; } = string.Empty; // PK
        public DateTime Payment_date { get; set; }
        public decimal Amount { get; set; }
        public string Payment_method { get; set; } = string.Empty;
        public string Rental_id { get; set; } = string.Empty; // FK

     
    }
}
