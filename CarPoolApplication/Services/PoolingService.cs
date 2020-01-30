using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public class PoolingService
    {
        UtilityService Service;

        public PoolingService()
        {
            Service = new UtilityService();
        }

        public Trip Create(Trip trip,string name)
        {
            trip.ID = Service.GenerateID();
            return trip;
        }


        public bool Delete(List<Trip> trips,Trip trip)
        {
            try
            {
                trips.Remove(trip);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Trip Update(Trip trip)
        {
            return trip;
        }

        public Trip Create(Trip trip)
        {
            trip.ID = Service.GenerateID();
            return trip;
        }
    }
}
