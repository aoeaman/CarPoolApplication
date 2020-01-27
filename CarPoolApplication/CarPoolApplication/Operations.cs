using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPoolApplication.Models;
using CarPoolApplication.Services;

namespace CarPoolApplication
{
    public class Operations
    {

        UserService UserServices = new UserService();
        BookingService BookingServices = new BookingService();
        PoolingService PoolingServices = new PoolingService();
        UtilityService Tools= new UtilityService();
        DataBase Data = new DataBase();

        bool AddDriver(string name,string userName,byte age,char gender,string phoneNumber,string password,string drivingLiscenceNumber)
        {
            if (!Data.Drivers.Any(Element => Element.Username == userName))
            {
                Driver NewDriver = new Driver()
                {
                    Name = name,
                    Username = userName,
                    Age = age,
                    Gender = gender,
                    PhoneNumber=phoneNumber,
                    Password=password,
                    DrivingLiscenceNumber=drivingLiscenceNumber
                };
                NewDriver = UserServices.CreateDriver(NewDriver);
                Data.Drivers.Add(NewDriver);
                return true;
            }
            return false;
        }




    }
}
