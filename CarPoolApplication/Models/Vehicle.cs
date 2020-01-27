using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolApplication
{
    class Vehicle
    {
        string ID { get; set; }
        string Maker { get; set; }
        string Number { get; set; }
        byte Seats { get; set; }
        VehicleType Type { get; set; }

    }
}
