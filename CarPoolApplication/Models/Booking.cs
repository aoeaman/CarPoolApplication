using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolApplication.Models
{
    public class Booking :CarPool
    {
        public decimal Fare { get; set; }
        public byte Seats { get; set; }
    }
}
