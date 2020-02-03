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
        
        IUserService UserServices = new UserService();
        IBookingService BookingServices = new BookingService();
        IOfferService PoolingServices = new OfferService();
        UtilityService Tools= new UtilityService();
        internal DataBase Data = new DataBase();
        UtilityService.Path Paths = new UtilityService.Path();

        public Operations()
        {
            Data.Drivers = JsonConvert.DeserializeObject<List<Driver>>(File.ReadAllText(Paths.Driver));
            Data.Riders = JsonConvert.DeserializeObject<List<Rider>>(File.ReadAllText(Paths.Rider));
            Data.Vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(File.ReadAllText(Paths.Vehicle));
            Data.Offers = JsonConvert.DeserializeObject<List<Offer>>(File.ReadAllText(Paths.Offer));
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
            vehicle = UserServices.RegisterUserVehicle(vehicle);
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
            Offer Offer=Data.Offers.Find(Element => Element.DriverID == driver.ID);
            PoolingServices.Delete(Data.Offers, Offer);
            SaveData(Paths.Offer,Data.Offers);
        }

        internal List<Offer> ShowRequests(Driver driver)
        {
             return Data.Offers.FindAll(Element => Element.DriverID == driver.ID);
        }

        internal List<Booking> GetRequests(Offer offer)
        {
            return offer.Bookings.FindAll(Element => Element.Status == StatusOfRide.Pending);
        }

        internal void GetBookingConfirmed(Offer offer, string confirmationID)
        {
            BookingServices.ConfirmRide(offer.Bookings.Find(Element => Element.RiderID == confirmationID));
            offer.Earnings += offer.Bookings.Find(Element => Element.RiderID == confirmationID).Fare;
            offer.SeatsAvailable -= offer.Bookings.Find(Element => Element.RiderID == confirmationID).Seats;
            SaveData(Paths.Offer,Data.Offers);
        }

        internal List<Offer> ViewOffers(Driver driver)
        {
            return Data.Offers.FindAll(Element=>Element.DriverID==driver.ID);
        }

        internal VehicleType GetVehilce(string vehicleID)
        {
            return Data.Vehicles.Find(Element=>Element.ID==vehicleID).Type;
        }

        internal void AddOffer(string vehicleID, string driverID,int source, int destinaiton, List<int> viaPoints, byte seats, string startDate, String endDate)
        {
            Offer Offer = SetOffer(vehicleID, driverID, source, destinaiton, viaPoints, seats, startDate, endDate);
            Offer = PoolingServices.Create(Offer);
            Data.Offers.Add(Offer);
            SaveData(Paths.Offer,Data.Offers);
        }

        private static Offer SetOffer(string vehicleID, string driverID, int source, int destinaiton, List<int> viaPoints, byte seats, string startDate, string endDate)
        {
            return new Offer()
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
                Bookings = new List<Booking>(),
                Earnings = 0,
                Requests = new List<string>(),
            };
        }

        internal decimal GetCharge(int source, int destinaiton)
        {
            return Math.Abs(Tools.Cities[source].First() + Tools.Cities[source].Last() - Tools.Cities[destinaiton].First() - Tools.Cities[destinaiton].Last()) * (decimal)15.99;
        }

        internal bool BookRide(string OfferID, Rider rider, int source, int destinaiton, decimal fare,byte seats)
        {
            Offer Offer = Data.Offers.Find(Name => Name.ID == OfferID);
            if (Offer.SeatsAvailable < seats && seats>0)
            {
                return false;
            }
            else
            {
                Offer.Requests.Add(rider.ID);
                Booking ride = SetRide(rider, source, destinaiton, fare*seats, seats, Offer);
                ride = BookingServices.CreateRide(ride);
                Offer.Bookings.Add(ride);               
                SaveData(Paths.Offer, Data.Offers);
                return true;
            }


        }

        private Booking SetRide(Rider rider, int source, int destinaiton, decimal fare, byte seats, Offer Offer)
        {
            return new Booking()
            {
                RiderID = rider.ID,
                Status = StatusOfRide.Pending,
                Source = source,
                Destination = destinaiton,
                Fare = fare,
                DriverID = Offer.DriverID,
                Seats = seats,
            };
        }

        internal List<Offer> GetAllOffers()
        {
            return Data.Offers.FindAll(Element=> Element.Status!= StatusOfRide.Completed);
        }

        internal List<Offer> GetOffers(int source, int destination)
        {
            List<Offer> Offers = new List<Offer>();
            foreach (var Offer in Data.Offers)
            {
               
                List<int> OfferSequence =new List<int>( Offer.ViaPoints);
                OfferSequence.Insert(0, Offer.Source);
                OfferSequence.Insert(OfferSequence.Count-1, Offer.Destination);
                if (OfferSequence.IndexOf(source)!=-1 && OfferSequence.IndexOf(source) < OfferSequence.IndexOf(destination))
                {
                    Offers.Add(Offer);
                }
            }
            return Offers;
                      
        }

        internal bool RemoveRide(string bookingID)
        {
            Offer Offer=Data.Offers.Find(Element => Element.Bookings.Any(Name => Name.ID == bookingID && Name.Status!= StatusOfRide.Completed && Name.Status!= StatusOfRide.Rejected));
            if (Offer == null)
            {
                return false;
            }
            else
            {
                BookingServices.CancelRide(Offer.Bookings.Find(Name => Name.ID == bookingID));
                Offer.Earnings -= Offer.Bookings.Find(Name => Name.ID == bookingID).Fare;
                SaveData(Paths.Offer, Data.Offers);
                return true;
            }
        }

        internal List<Booking> GetBookings(Rider rider)
        {
            List<Booking> rides=new List<Booking>();
            foreach (var Element in Data.Offers)
            {
                Booking ride = Element.Bookings.Find(Name => Name.RiderID == rider.ID);
                if (ride != null)
                {
                    rides.Add(ride);
                }
                
            }
            return rides;
        }

        internal void CompleteOffer(Offer offer)
        {
            offer.Status = StatusOfRide.Completed;
            offer.Bookings.ForEach(Element => {
                if (Element.Status== StatusOfRide.Pending)
                {
                    Element.Status = StatusOfRide.Rejected;
                }
                else
                {
                    Element.Status = StatusOfRide.Completed;
                }
            }
            );
            SaveData(Paths.Offer,Data.Offers);
        }
    }
}