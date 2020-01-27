using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolApplication
{
    class CarPool
    {
        string ID { get; set; }
        StatusOfRide Status { get; set; }
        string Source { get; set; }
        string Destination { get; set; }
    }
}
