using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public interface IPooling
    {
        Trip Create(Trip trip);

        bool Delete(List<Trip> trips, Trip trip);

        Trip Update(Trip trip);
    }
}
