namespace RentCarApi.Models
{
    public class TrMaintenance
    {
        public string Maintenance_id { get; set; } = string.Empty; 
        public DateTime Maintenance_date { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public string Car_id { get; set; } = string.Empty; 
        public string Employee_id { get; set; } = string.Empty; 

       
    }
}
