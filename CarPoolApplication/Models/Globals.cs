using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolApplication.Models
{
    public enum StatusOfRide
    {
        Pending,
        Accepted,
        Rejected,
        Cancelled,
        Completed,
        Created,
        Started
    }

    public enum VehicleType
    {
        Bike,
        Car,
        Jeep
    }
}
