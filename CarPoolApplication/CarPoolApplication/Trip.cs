using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolApplication
{
    class Trip:CarPool
    {
        byte SeatsAvailable { get; set; }
        decimal Earnings { get; set; }
        List<Booking> Bookings { get; set; }
        List<string> ViaPoints { get; set; }
        string CurrentCity { get; set; }       
        string StartDate { get; set; }
        string EndDate { get; set; }

    }
}
