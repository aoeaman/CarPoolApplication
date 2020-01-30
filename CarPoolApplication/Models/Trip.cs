using System.Collections.Generic;

namespace CarPoolApplication.Models
{
    public class Trip :CarPool
    {
        public string DriverID { get; set; }
        public string VehicleID { get; set; }
        public byte SeatsAvailable { get; set; }
        public decimal Earnings { get; set; }
        public List<string> Requests { get; set; }
        public List<Ride> Bookings { get; set; }
        public List<int> ViaPoints { get; set; }
        public int CurrentCity { get; set; }
        public string EndDate { get; set; }
        
    }
}
