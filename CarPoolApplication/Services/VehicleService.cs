using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CarPoolApplication.Models;
using Newtonsoft.Json;

namespace CarPoolApplication.Services
{
    class VehicleService:ICommonService<Vehicle>
    {
        UtilityService Service;
        private readonly string VehiclePath = "C:\\repos\\CarPoolApplication\\CarPoolApplication\\Vehicle.JSON";
        private List<Vehicle> Vehicles;

        public VehicleService()
        {
            Service = new UtilityService();
        }

        private List<Vehicle> GetVehicles()
        {
            return Vehicles;
        }

        private void SetVehicles(List<Vehicle> value)
        {
            Vehicles=JsonConvert.DeserializeObject<List<Vehicle>>(File.ReadAllText(VehiclePath));
        }

        public void Add(Vehicle vehicle)
        {
            GetVehicles().Add(vehicle);
        }
        public Vehicle Create(Vehicle vehicle)
        {
            vehicle.ID = Service.GenerateID();
            return vehicle;
        }

        public IList<Vehicle> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
