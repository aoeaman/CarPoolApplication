﻿using System.Collections.Generic;
using System.IO;
using CarPoolApplication.Models;
using Newtonsoft.Json;

namespace CarPoolApplication.Services
{
    public class BookingService:IBookingService
    {
        UtilityService Service;

        private List<Booking> Bookings;
        public readonly string BookingPath = "C:\\repos\\CarPoolApplication\\CarPoolApplication\\Booking.JSON";

        public BookingService()
        {
            Service = new UtilityService();
            Bookings= JsonConvert.DeserializeObject<List<Booking>>(File.ReadAllText(BookingPath)) ?? new List<Booking>();
        }

        public void UpdateStatus(Booking ride,StatusOfRide status)
        {
            ride.Status =status;
        }
      
        public void Add(Booking entity)
        {
            Bookings.Add(entity);
        }

        public Booking Create(Booking entity)
        {
            entity.ID = Service.GenerateID();
            return entity;
        }

        public List<Booking> GetAll()
        {
            return Bookings;
        }
        public void Delete(string iD)
        {
            Bookings.Remove(Bookings.Find(_ => _.ID == iD));
        }

        public void SaveData()
        {
            File.WriteAllText(BookingPath, JsonConvert.SerializeObject(Bookings));
        }
    }
}
