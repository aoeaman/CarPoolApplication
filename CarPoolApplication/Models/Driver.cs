using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolApplication.Models
{
    public class Driver :User
    {
        public List<string> VehicleIDs { get; set; }
        public string DrivingLiscenceNumber { get; set; }
    }
}
