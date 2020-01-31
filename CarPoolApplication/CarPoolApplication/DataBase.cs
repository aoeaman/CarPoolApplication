using System.Collections.Generic;
using CarPoolApplication.Models;

namespace CarPoolApplication
{
    public class DataBase
    {
        public List<Offer> Offers = new List<Offer>();
        public List<Driver> Drivers = new List<Driver>();
        public List<Rider> Riders = new List<Rider>();
        public List<Vehicle> Vehicles = new List<Vehicle>();
    }
}
