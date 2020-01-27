using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolApplication.Models
{
    public class Trip :CarPool
    {
        public byte SeatsAvailable { get; set; }
        public decimal Earnings { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<string> ViaPoints { get; set; }
        public string CurrentCity { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        
    }
}
