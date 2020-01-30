using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public interface IUser
    {
        Vehicle RegisterVehicle(Vehicle vehicle);

        Driver CreateDriver(Driver driver);

        Rider CreateRider(Rider rider);

    }
}
