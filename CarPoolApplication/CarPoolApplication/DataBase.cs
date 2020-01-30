using System.Collections.Generic;
using CarPoolApplication.Models;

namespace CarPoolApplication
{
    public class DataBase
    {
        public List<Trip> Trips = new List<Trip>();
        public List<Driver> Drivers = new List<Driver>();
        public List<Rider> Riders = new List<Rider>();
        public List<Vehicle> Vehicles = new List<Vehicle>();
    }
}
