using System.Collections.Generic;

namespace CarPoolApplication.Models
{
    public class Offer
    {
        public string ID { get; set; }
        public StatusOfRide Status { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
        public string StartDate { get; set; }
        public string DriverID { get; set; }
        public string VehicleID { get; set; }
        public byte SeatsAvailable { get; set; }
        public decimal Earnings { get; set; }
        public List<string> Requests { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<int> ViaPoints { get; set; }       
        public string EndDate { get; set; }       
    }
}
