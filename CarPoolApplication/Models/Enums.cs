using System;
using System.Collections.Generic;
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
        Completed
    }

    public enum VehicleType
    {
        Bike,
        Car,
        Jeep
    }
}
