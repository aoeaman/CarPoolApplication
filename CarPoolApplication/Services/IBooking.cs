using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public interface IBooking
    {
        void ConfirmRide(Ride ride);

        void CancelRide(Ride ride);

        Ride CreateRide(Ride ride);
    }
}
