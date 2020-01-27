using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolApplication
{
    public class Booking :CarPool
    {
        decimal Fare { get; set; }
        byte Seats { get; set; }
    }
}
