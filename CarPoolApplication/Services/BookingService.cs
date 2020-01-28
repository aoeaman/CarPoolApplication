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

        public void ConfirmRide(Ride ride)
        {
            ride.Status = StatusOfRide.Accepted;
        }

        public void CancelRide(Trip trip,Ride ride)
        {
            trip.Bookings.Remove(ride);
        }
    }
}
