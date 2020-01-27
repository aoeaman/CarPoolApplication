using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public class UserService
    {
        UtilityService Service;
        public UserService()
        {
            Service = new UtilityService();
        }

        public Driver CreateDriver(Driver driver)
        {
            driver.ID = Service.GenerateID();
            return driver;
        }
    }
}
