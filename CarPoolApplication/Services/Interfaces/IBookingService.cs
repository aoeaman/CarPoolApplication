using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    public interface IBookingService
    {
        void UpdateStatus(Booking ride, StatusOfRide status);
        Booking Create(Booking ride);
        void Add(Booking ride);
        List<Booking> GetAll();
        void Delete(string iD);
    }
}
