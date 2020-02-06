using System;
using System.Collections.Generic;
using System.IO;
using CarPoolApplication.Models;
using Newtonsoft.Json;
using CarPoolApplication.Services.Interfaces;

namespace CarPoolApplication.Services
{
    public class DriverService:IUserService<Driver>
    {
        UtilityService Service;
        List<Driver> Drivers;
        public readonly string DriverPath = "C:\\repos\\CarPoolApplication\\CarPoolApplication\\Driver.JSON";
        public DriverService()
        {
            Service = new UtilityService();
            Drivers= JsonConvert.DeserializeObject<List<Driver>>(File.ReadAllText(DriverPath)) ?? new List<Driver>();
        }

        public void Add(Driver driver)
        {
            Drivers.Add(driver);
        }

        public Driver Create(Driver driver)
        {
            driver.ID = Service.GenerateID();
            return driver;
        }

        public List<Driver> GetAll()
        {
            return Drivers;
        }

        public void SaveData()
        {
            File.WriteAllText(DriverPath, JsonConvert.SerializeObject(Drivers));
        }
    }
}
