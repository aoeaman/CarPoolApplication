using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolApplication.Models
{
    public class CarPool
    {
        public string ID { get; set; }
        public StatusOfRide Status { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
        public string StartDate{get;set;}
    }
}
