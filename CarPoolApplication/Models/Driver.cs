using System.Collections.Generic;

namespace CarPoolApplication.Models
{
    public class Driver :User
    {
        public List<string> VehicleIDs { get; set; }
        public string DrivingLiscenceNumber { get; set; }
    }
}
