using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CarPoolApplication.Models;
using Newtonsoft.Json;

namespace CarPoolApplication.Services
{
    public class DriverService:ICommonService<Driver>
    {
        UtilityService Service;
        List<Driver> Drivers;
        public readonly string DriverPath = "C:\\repos\\CarPoolApplication\\CarPoolApplication\\Driver.JSON";
        public DriverService()
        {
            Service = new UtilityService();
            Drivers= JsonConvert.DeserializeObject<List<Driver>>(File.ReadAllText(DriverPath));
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

        public IList<Driver> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
