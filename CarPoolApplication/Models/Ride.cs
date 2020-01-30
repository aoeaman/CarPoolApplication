namespace CarPoolApplication.Models
{
    public class Ride :CarPool
    {
        public string RiderID { get; set; }
        public string DriverID { get; set; }
        public decimal Fare { get; set; }
        public byte Seats { get; set; }
    }
}
