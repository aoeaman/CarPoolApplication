using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CarPoolApplication.Models;
using Newtonsoft.Json;

namespace CarPoolApplication.Services
{
    public class VehicleService:ICommonService<Vehicle>
    {
        UtilityService Service;
        private readonly string VehiclePath = "C:\\repos\\CarPoolApplication\\CarPoolApplication\\Vehicle.JSON";
        private List<Vehicle> Vehicles;

        public VehicleService()
        {
            Service = new UtilityService();
            Vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(File.ReadAllText(VehiclePath)) ?? new List<Vehicle>();
        }

        public void Add(Vehicle vehicle)
        {
            Vehicles.Add(vehicle);
        }
        public Vehicle Create(Vehicle vehicle)
        {
            vehicle.ID = Service.GenerateID();
            return vehicle;
        }

        public List<Vehicle> GetAll()
        {
            return Vehicles;
        }

        public List<Vehicle> GetAllByID(string ID)
        {
            return Vehicles.FindAll(_ => _.DriverID == ID);
        }
    }
}
