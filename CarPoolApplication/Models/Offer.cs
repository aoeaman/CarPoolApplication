﻿using System;
using System.Collections.Generic;

namespace CarPoolApplication.Models
{
    public class Offer
    {
        public string ID { get; set; }
        public StatusOfRide Status { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
        public int CurrentLocaton { get; set; }       
        public string DriverID { get; set; }
        public string VehicleID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte SeatsAvailable { get; set; }
        public decimal Earnings { get; set; }
        public List<int> ViaPoints { get; set; }      
    }
}
