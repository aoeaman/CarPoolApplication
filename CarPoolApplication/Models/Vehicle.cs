using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolApplication.Models
{
    public class Vehicle
    {
        public string ID { get; set; }
        public string Maker { get; set; }
        public string Number { get; set; }
        public byte Seats { get; set; }
        public VehicleType Type { get; set; }
    }
}
