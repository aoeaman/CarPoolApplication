using System;
using CarPoolApplication.Models;
namespace CarPoolApplication.Services
{
    public class BookingService
    {
        UtilityService Service;
        public BookingService()
        {
            Service = new UtilityService();
        }

        public Ride ConfirmRide(Ride ride,string name)
        {
            ride.ID = Service.GenerateID();
            return ride;
        }

        public void CancelRide(Trip trip,Ride ride)
        {
            trip.Bookings.Remove(ride);
        }
    }
}
