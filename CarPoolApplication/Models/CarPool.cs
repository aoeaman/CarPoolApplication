﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolApplication.Models
{
    public class Book
    {
        public string ID { get; set; }
        public StatusOfRide Status { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string StartDate{get;set;}
    }
}
