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
        
        ICommonService<Driver> DriverServices = new DriverService();
        ICommonService<Rider> RiderServices = new RiderService();
        IBookingService BookingServices = new BookingService();
        IOfferService OfferServices = new OfferService();
        VehicleService VehicleServices = new VehicleService();
        UtilityService Tools= new UtilityService();
        UtilityService.Path Paths = new UtilityService.Path();

        internal void SaveData<T>(string path, T t)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(t));
        }

        internal void AddDriver(string name,string userName,byte age,char gender,string phoneNumber,string password,string drivingLiscenceNumber)
        {
            if (!DriverServices.GetAll().Any(Element => Element.Username == userName))
            {
                Driver NewDriver = SetDriver(name, userName, age, gender, phoneNumber, password, drivingLiscenceNumber);
                NewDriver = DriverServices.Create(NewDriver);               
                DriverServices.Add(NewDriver);
                SaveData(Paths.Driver, DriverServices.GetAll());
            }
            else
            {
                throw new Exception();
            }
        }

        internal Rider GetRider(string userName, string password)
        {
            return RiderServices.GetAll().Find(Element => Element.Username == userName && Element.Password == password);
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
            };
        }

        internal void AddRider(string name, string userName, byte age, char gender, string phoneNumber, string password)
        {
            if (!RiderServices.GetAll().Any(Element => Element.Username == userName))
            {
                Rider NewRider = SetRider(name, userName, age, gender, phoneNumber, password);
                NewRider = RiderServices.Create(NewRider);
                RiderServices.Add(NewRider);
                SaveData(Paths.Rider, RiderServices.GetAll());
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

        internal Driver GetDriver(string userName, string password)
        {
            return DriverServices.GetAll().Find(Element => Element.Username == userName && Element.Password == password);
        }

        internal void AddVehicle(string driverID,string vehicleNumber,string maker,VehicleType type,byte seats)
        {
            Vehicle vehicle = SetVehicle(vehicleNumber, maker, type, seats,driverID);
            vehicle = VehicleServices.Create(vehicle);          
            VehicleServices.Add(vehicle);
            SaveData(Paths.Vehicle, VehicleServices.GetAll());

        }

        private static Vehicle SetVehicle(string vehicleNumber, string maker, VehicleType type, byte seats,string driverID)
        {
            return new Vehicle()
            {
                Number = vehicleNumber,
                Maker = maker,
                Type = type,
                Seats = seats,
                DriverID=driverID
            };
        }

        internal void DeleteOffer(string offerID)
        {
            OfferServices.Delete(offerID);
            SaveData(Paths.Offer, OfferServices.GetAll());
        }

        internal List<Offer> ShowRequests(string driverID)
        {          
            return OfferServices.GetAll().FindAll(Element => Element.DriverID == driverID);
        }

        internal List<Booking> GetRequests(string offerID)
        {
            return BookingServices.GetAll().FindAll(Element => Element.Status == StatusOfRide.Pending && Element.OfferID==offerID);
        }

        internal void GetBookingConfirmed(string offerID, string confirmationID)
        {
            var Booking_ = BookingServices.GetAll().Find(Element => Element.RiderID == confirmationID && Element.OfferID == offerID);
            var Offer_ = OfferServices.GetAll().Find(_=>_.ID==offerID);
            BookingServices.ConfirmRide(Booking_);
            Offer_.Earnings += Booking_.Fare;
            Offer_.SeatsAvailable -= Booking_.Seats;
            SaveData(Paths.Offer, OfferServices.GetAll());
            SaveData(Paths.Booking,BookingServices.GetAll());
        }

        internal List<Offer> ViewOffers(string driverID)
        {
            var Data = OfferServices.GetAll();
            return Data.FindAll(Element=>Element.DriverID==driverID);
        }

        internal void AddOffer(string vehicleID, string driverID,int source, int destinaiton, List<int> viaPoints, byte seats, string startDate, String endDate)
        {
            
            Offer Offer = SetOffer(vehicleID, driverID, source, destinaiton, viaPoints, seats, startDate, endDate);
            Offer = OfferServices.Create(Offer);
            OfferServices.Add(Offer);
            SaveData(Paths.Offer,OfferServices.GetAll());
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
            Offer Offer = OfferServices.GetByID(OfferID);
            if (Offer==null || Offer.SeatsAvailable < seats && seats>0)
            {
                return false;
            }
            else
            {
                Offer.Requests.Add(rider.ID);
                Booking ride = SetRide(rider, source, destinaiton, fare*seats, seats, Offer);
                ride = BookingServices.Create(ride);
                BookingServices.Add(ride);              
                SaveData(Paths.Booking, BookingServices.GetAll());
                SaveData(Paths.Offer, OfferServices.GetAll());
                return true;
            }


        }

        private Booking SetRide(Rider rider, int source, int destinaiton, decimal fare, byte seats, Offer offer)
        {
            return new Booking()
            {
                RiderID = rider.ID,
                Status = StatusOfRide.Pending,
                Source = source,
                Destination = destinaiton,
                Fare = fare,
                OfferID = offer.ID,
                Seats = seats,
            };
        }

        internal List<Offer> GetAllAvailableOffers()
        {
            return OfferServices.GetAll().FindAll(Element=> Element.Status!= StatusOfRide.Completed);
        }

        internal List<Offer> GetOffers(int source, int destination)
        {
            List<Offer> Offers = new List<Offer>();
            foreach (var Offer in OfferServices.GetAll())
            {
               
                List<int> OfferSequence =new List<int>( Offer.ViaPoints);
                OfferSequence.Insert(0, Offer.Source);
                OfferSequence.Insert(OfferSequence.Count, Offer.Destination);
                if (OfferSequence.IndexOf(source)!=-1 && OfferSequence.IndexOf(source) < OfferSequence.IndexOf(destination))
                {
                    Offers.Add(Offer);
                }
            }
            return Offers;
                      
        }

        internal bool RemoveRide(string bookingID)
        {
            var Booking_ = BookingServices.GetAll().FirstOrDefault(_=>_.ID==bookingID);
            var Data = OfferServices.GetAll();
            Offer Offer=Data.FirstOrDefault(Element => Element.ID==Booking_.OfferID);
            if (Booking_ == null || Booking_.Status !=  StatusOfRide.Pending)
            {
                return false;
            }
            else
            {
                BookingServices.CancelRide(Booking_);
                Offer.Earnings -=Booking_.Fare;
                SaveData(Paths.Booking, BookingServices.GetAll());
                SaveData(Paths.Offer, Data);
                return true;
            }
        }

        internal List<Booking> GetBookings(string riderID)
        {
            return BookingServices.GetAll().FindAll(_ => _.RiderID == riderID);
        }

        internal void CompleteOffer(string offerID)
        {
            var Offer_ = OfferServices.GetAll().Find(_ => _.ID == offerID);
            Offer_.Status = StatusOfRide.Completed;
            BookingServices.GetAll().FindAll(_ => _.OfferID == Offer_.ID).ForEach(Element => {
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
            SaveData(Paths.Offer,OfferServices.GetAll());
        }

        internal List<Vehicle> GetDriverVehicles(string driverID)
        {
            return VehicleServices.GetAll().FindAll(_ => _.DriverID == driverID);
        }

        internal int GetRidersCount(string offeriD)
        {
            return BookingServices.GetAll().FindAll(_=>_.OfferID==offeriD && _.Status!= StatusOfRide.Cancelled).Count;
        }
    }
}