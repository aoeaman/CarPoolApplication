using System;
using System.Collections.Generic;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public class PoolingService:IPooling
    {
        UtilityService Service;

        public PoolingService()
        {
            Service = new UtilityService();
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

        public Trip Create(Trip trip)
        {
            trip.ID = Service.GenerateID();
            return trip;
        }

        public Trip Update(Trip trip)
        {
            throw new NotImplementedException();
        }
    }
}
