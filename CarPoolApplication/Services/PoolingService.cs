using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public class PoolingService:IPooling
    {
        Trip Create(Trip trip)
        {
            trip.ID = "10221";
            return trip;
        }
    }
}
