using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPoolApplication.Models;
using CarPoolApplication.Services;

namespace CarPoolApplication
{
    public class Operations
    {
        UserService UserServices = new UserService();
        BookingService BookingServices = new BookingService();
        PoolingService PoolingServices = new PoolingService();
        UtilityService Tools= new UtilityService();
        internal DataBase Data = new DataBase();

        internal void AddDriver(string name,string userName,byte age,char gender,string phoneNumber,string password,string drivingLiscenceNumber)
        {
            if (!Data.Drivers.Any(Element => Element.Username == userName))
            {
                Driver NewDriver = new Driver()
                {
                    Name = name,
                    Username = userName,
                    Age = age,
                    Gender = gender,
                    PhoneNumber = phoneNumber,
                    Password = password,
                    DrivingLiscenceNumber = drivingLiscenceNumber.ToUpper()
                };
                NewDriver = UserServices.CreateDriver(NewDriver);
                Data.Drivers.Add(NewDriver);
            }
            else
            {
                throw new Exception();
            }
        }

        internal void AddRider(string name, string userName, byte age, char gender, string phoneNumber, string password)
        {
            if (!Data.Riders.Any(Element => Element.Username == userName))
            {
                Rider NewRider = new Rider()
                {
                    Name = name,
                    Username = userName,
                    Age = age,
                    Gender = gender,
                    PhoneNumber = phoneNumber,
                    Password = password
                };
                NewRider = UserServices.CreateRider(NewRider);
                Data.Riders.Add(NewRider);
            }
            else
            {
                throw new Exception();
            }
        }

        internal void AddVehicle(Driver driver,string vehicleNumber,string maker,VehicleType type,byte seats)
        {
            Vehicle vehicle = new Vehicle()
            {
                Number = vehicleNumber,
                Maker = maker,
                Type = type,
                Seats = seats
            };
            vehicle = UserServices.RegisterVehicle(vehicle);
            driver.VehicleIDs.Add(vehicle.ID);
            Data.Vehicles.Add(vehicle);
        }

        internal void DeleteOffer(Driver driver, string offerID)
        {
            Trip trip=Data.Trips.Find(Element => Element.DriverID == driver.ID);
            PoolingServices.Delete(Data.Trips, trip);
        }

        internal List<Trip> ShowRequests(Driver driver)
        {
             return Data.Trips.FindAll(Element => Element.DriverID == driver.ID);
        }

        internal List<Ride> GetRequests(Trip trip)
        {
            return trip.Bookings.FindAll(Element => Element.Status == StatusOfRide.Pending);
        }

        internal void GetBookingConfirmed(Trip trip, string confirmationID)
        {
            BookingServices.ConfirmRide(trip.Bookings.Find(Element => Element.RiderID == confirmationID));
        }

        internal List<Trip> ViewOffers(Driver driver)
        {
            return Data.Trips.FindAll(Element=>Element.DriverID==driver.ID);
        }
    }
}