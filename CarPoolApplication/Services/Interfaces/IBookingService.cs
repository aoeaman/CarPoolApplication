using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public interface IBookingService
    {
        void ConfirmRide(Booking ride);
        void CancelRide(Booking ride);
        Booking Create(Booking ride);
        void Add(Booking ride);
        List<Booking> GetAll();
        void Delete(string iD);
    }
}
