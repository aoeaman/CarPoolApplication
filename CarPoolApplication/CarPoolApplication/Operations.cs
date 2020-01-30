using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarPoolApplication.Models;
using CarPoolApplication.Services;
using Newtonsoft.Json;

namespace CarPoolApplication
{
    public class Operations
    {
        
        UserService UserServices = new UserService();
        BookingService BookingServices = new BookingService();
        PoolingService PoolingServices = new PoolingService();
        UtilityService Tools= new UtilityService();
        internal DataBase Data = new DataBase();
        UtilityService.Path Paths = new UtilityService.Path();

        public Operations()
        {
            //string S = JsonConvert.SerializeObject(new List<Driver>());
            //File.WriteAllText(Paths.Driver, S);
            //string S1 = JsonConvert.SerializeObject(new List<Trip>());
            //File.WriteAllText(Paths.Trip, S1);
            //string S2 = JsonConvert.SerializeObject(new List<Vehicle>());
            //File.WriteAllText(Paths.Vehicle, S2);
            //string S3 = JsonConvert.SerializeObject(new List<Rider>());
            //File.WriteAllText(Paths.Rider, S3);

            Data.Drivers = JsonConvert.DeserializeObject<List<Driver>>(File.ReadAllText(Paths.Driver));
            Data.Riders = JsonConvert.DeserializeObject<List<Rider>>(File.ReadAllText(Paths.Rider));
            Data.Vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(File.ReadAllText(Paths.Vehicle));
            Data.Trips = JsonConvert.DeserializeObject<List<Trip>>(File.ReadAllText(Paths.Trip));
        }

        internal void SaveData<T>(string path, T t)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(t));
        }

        internal void AddDriver(string name,string userName,byte age,char gender,string phoneNumber,string password,string drivingLiscenceNumber)
        {
            if (!Data.Drivers.Any(Element => Element.Username == userName))
            {
                Driver NewDriver = SetDriver(name, userName, age, gender, phoneNumber, password, drivingLiscenceNumber);
                NewDriver = UserServices.CreateDriver(NewDriver);               
                Data.Drivers.Add(NewDriver);
                SaveData(Paths.Driver,Data.Drivers);
            }
            else
            {
                throw new Exception();
            }
        }

        private static Driver SetDriver(string name, string userName, byte age, char gender, string phoneNumber, string password, string drivingLiscenceNumber)
        {
            return new Driver()
            {
                Name = name,
                Username = userName,
                Age = age,
                Gender = gender,
                PhoneNumber = phoneNumber,
                Password = password,
                DrivingLiscenceNumber = drivingLiscenceNumber.ToUpper(),
                VehicleIDs = new List<string>()
                
            };
        }

        internal void AddRider(string name, string userName, byte age, char gender, string phoneNumber, string password)
        {
            if (!Data.Riders.Any(Element => Element.Username == userName))
            {
                Rider NewRider = SetRider(name, userName, age, gender, phoneNumber, password);
                NewRider = UserServices.CreateRider(NewRider);
                Data.Riders.Add(NewRider);
                SaveData(Paths.Rider,Data.Riders);
            }
            else
            {
                throw new Exception();
            }
        }

        private static Rider SetRider(string name, string userName, byte age, char gender, string phoneNumber, string password)
        {
            return new Rider()
            {
                Name = name,
                Username = userName,
                Age = age,
                Gender = gender,
                PhoneNumber = phoneNumber,
                Password = password
            };
        }

        internal void AddVehicle(Driver driver,string vehicleNumber,string maker,VehicleType type,byte seats)
        {
            Vehicle vehicle = SetVehicle(vehicleNumber, maker, type, seats);
            vehicle = UserServices.RegisterVehicle(vehicle);
            driver.VehicleIDs.Add(vehicle.ID);
            Data.Vehicles.Add(vehicle);
            SaveData(Paths.Vehicle,Data.Vehicles);
            SaveData(Paths.Driver,Data.Drivers);

        }

        private static Vehicle SetVehicle(string vehicleNumber, string maker, VehicleType type, byte seats)
        {
            return new Vehicle()
            {
                Number = vehicleNumber,
                Maker = maker,
                Type = type,
                Seats = seats
            };
        }

        internal void DeleteOffer(Driver driver, string offerID)
        {
            Trip trip=Data.Trips.Find(Element => Element.DriverID == driver.ID);
            PoolingServices.Delete(Data.Trips, trip);
            SaveData(Paths.Trip,Data.Trips);
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
            trip.Earnings += trip.Bookings.Find(Element => Element.RiderID == confirmationID).Fare;
            SaveData(Paths.Trip,Data.Trips);
        }

        internal List<Trip> ViewOffers(Driver driver)
        {
            return Data.Trips.FindAll(Element=>Element.DriverID==driver.ID);
        }

        internal VehicleType GetVehilce(string vehicleID)
        {
            return Data.Vehicles.Find(Element=>Element.ID==vehicleID).Type;
        }

        internal void AddOffer(string vehicleID, string driverID,int source, int destinaiton, List<int> viaPoints, byte seats, string startDate, String endDate)
        {
            Trip trip = SetTrip(vehicleID, driverID, source, destinaiton, viaPoints, seats, startDate, endDate);
            trip = PoolingServices.Create(trip);
            Data.Trips.Add(trip);
            SaveData(Paths.Trip,Data.Trips);
        }

        private static Trip SetTrip(string vehicleID, string driverID, int source, int destinaiton, List<int> viaPoints, byte seats, string startDate, string endDate)
        {
            return new Trip()
            {
                VehicleID = vehicleID,
                Source = source,
                Destination = destinaiton,
                ViaPoints = viaPoints,
                SeatsAvailable = seats,
                StartDate = startDate,
                EndDate = endDate,
                Status = StatusOfRide.Created,
                DriverID = driverID,
                Bookings = new List<Ride>(),
                Earnings = 0,
                Requests = new List<string>(),
                CurrentCity = source
            };
        }

        internal decimal GetCharge(int source, int destinaiton)
        {
            return Math.Abs(Tools.Cities[source].First() + Tools.Cities[source].Last() - Tools.Cities[destinaiton].First() - Tools.Cities[destinaiton].Last()) * (decimal)15.99;
        }

        internal bool BookRide(string tripID, Rider rider, int source, int destinaiton, decimal fare,byte seats)
        {
            Trip trip = Data.Trips.Find(Name => Name.ID == tripID);
            if (trip.SeatsAvailable < seats)
            {
                return false;
            }
            else
            {
                trip.Requests.Add(rider.ID);
                Ride ride = SetRide(rider, source, destinaiton, fare, seats, trip);
                ride = BookingServices.CreateRide(ride);
                trip.Bookings.Add(ride);
                trip.SeatsAvailable -= seats;
                SaveData(Paths.Trip, Data.Trips);
                return true;
            }


        }

        private Ride SetRide(Rider rider, int source, int destinaiton, decimal fare, byte seats, Trip trip)
        {
            return new Ride()
            {
                RiderID = rider.ID,
                Status = StatusOfRide.Pending,
                Source = source,
                Destination = destinaiton,
                Fare = fare,
                DriverID = trip.DriverID,
                Seats = seats,
                StartDate = trip.StartDate
            };
        }

        internal List<Trip> GetAllOffers()
        {
            return Data.Trips;
        }

        internal List<Trip> GetTrips(int source, int destination)
        {
            List<Trip> Trips = new List<Trip>();
            foreach (var trip in Data.Trips)
            {
               
                List<int> TripSequence =new List<int>( trip.ViaPoints);
                TripSequence.Insert(0, trip.Source);
                TripSequence.Insert(TripSequence.Count, trip.Destination);
                if (TripSequence.IndexOf(source) < TripSequence.IndexOf(destination))
                {
                    Trips.Add(trip);
                }
            }
            return Trips;
                      
        }

        internal bool RemoveRide(string bookingID)
        {
            Trip Trip=Data.Trips.Find(Element => Element.Bookings.Any(Name => Name.ID == bookingID));
            if (Trip == null)
            {
                return false;
            }
            else
            {
                BookingServices.CancelRide(Trip.Bookings.Find(Name => Name.ID == bookingID));
                Trip.Earnings -= Trip.Bookings.Find(Name => Name.ID == bookingID).Fare;
                SaveData(Paths.Trip, Data.Trips);
                return true;
            }
        }

        internal List<Ride> GetBookings(Rider rider)
        {
            List<Ride> rides=new List<Ride>();
            foreach (var Element in Data.Trips)
            {
                Ride ride = Element.Bookings.Find(Name => Name.RiderID == rider.ID);
                if (ride != null)
                {
                    rides.Add(ride);
                }
                
            }
            return rides;
        }
    }
}