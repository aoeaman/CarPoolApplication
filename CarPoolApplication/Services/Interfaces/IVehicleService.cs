using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services.Interfaces
{
    public interface IVehicleService
    {
        void Add(Vehicle entity);
        Vehicle Create(Vehicle entity);
        List<Vehicle> GetAll();
        Vehicle GetVehicleByID(string iD);
        void SaveData();
    }
}
