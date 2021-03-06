﻿using System;
using System.Collections.Generic;
using System.Linq;
using CarPoolApplication.Models;
using CarPoolApplication.Services;
using CarPoolApplication.Services.Interfaces;

namespace CarPoolApplication
{
    public class Operations
    {
        
        IUserService<Driver> DriverServices = new DriverService();
        IUserService<Rider> RiderServices = new RiderService();
        IBookingService BookingServices = new BookingService();
        IOfferService OfferServices = new OfferService();
        IVehicleService VehicleServices = new VehicleService();      

        internal void AddDriver(string name,string userName,byte age,char gender,string phoneNumber,string password,string drivingLiscenceNumber)
        {
            if (!DriverServices.GetAll().Any(Element => Element.Username == userName))
            {
                Driver NewDriver = SetDriver(name, userName, age, gender, phoneNumber, password, drivingLiscenceNumber);
                NewDriver = DriverServices.Create(NewDriver);               
                DriverServices.Add(NewDriver);
                DriverServices.SaveData();
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
                RiderServices.SaveData();
            }
            else
            {
                throw new Exception();
            }
        }

        internal object PasswordHelper<T>(string userName, string phoneNumber)
        {           
            if (typeof(T) == typeof(Driver))
            {
                return DriverServices.GetAll().FirstOrDefault(_ => _.Username == userName && _.PhoneNumber == phoneNumber);

            }
            else
            {
                return RiderServices.GetAll().FirstOrDefault(_ => _.Username == userName && _.PhoneNumber == phoneNumber);
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
            VehicleServices.SaveData();

        }

        private static Vehicle SetVehicle(string vehicleNumber, string maker, VehicleType type, byte seats,string driverID)
        {
            return new Vehicle()
            {
                Number = vehicleNumber,
                Maker = maker,
                Type = type,
                Seats = seats,
                DriverID=driverID,
                IsActive=true
            };
        }

        internal void DeleteOffer(string offerID)
        {
            OfferServices.Delete(offerID);
            OfferServices.SaveData();
        }

        internal List<Offer> GetOfferByDriverID(string driverID)
        {          
            return OfferServices.GetAll().FindAll(Element => Element.DriverID == driverID && Element.Status== StatusOfRide.Created);
        }

        internal List<Booking> GetRequests(string offerID)
        {
            return BookingServices.GetAll().FindAll(Element => Element.Status == StatusOfRide.Pending && Element.OfferID==offerID);
        }

        internal void GetBookingConfirmed(string offerID, string confirmationID)
        {
            var Booking_ = BookingServices.GetAll().Find(Element => Element.ID == confirmationID);
            var Offer_ = OfferServices.GetAll().Find(_=>_.ID==offerID);            
            BookingServices.UpdateStatus(Booking_,StatusOfRide.Accepted);
            Offer_.Earnings += Booking_.Fare;
            OfferServices.SaveData();
            BookingServices.SaveData();
        }

        internal void SetNewPassword(Driver user,string password)
        {
            user.Password = password;
            DriverServices.SaveData();
        }

        internal void SetNewPassword(Rider user, string password)
        {
            user.Password = password;
            RiderServices.SaveData();
        }

        internal List<Offer> ViewOffers(string driverID)
        {
            var Data = OfferServices.GetAll();
            return Data.FindAll(Element=>Element.DriverID==driverID);
        }

        internal void AddOffer(string vehicleID, string driverID,int source, int destinaiton, List<int> viaPoints, byte seats, DateTime startDate, DateTime endDate)
        {
            
            Offer Offer = SetOffer(vehicleID, driverID, source, destinaiton, viaPoints, seats, startDate, endDate);
            Offer = OfferServices.Create(Offer);
            OfferServices.Add(Offer);
            OfferServices.SaveData();
        }

        private static Offer SetOffer(string vehicleID, string driverID, int source, int destinaiton, List<int> viaPoints, byte seats, DateTime startDate, DateTime endDate)
        {
            return new Offer()
            {
                VehicleID = vehicleID,
                Source = source,
                Destination = destinaiton,
                CurrentLocaton=source,
                ViaPoints = viaPoints,
                SeatsAvailable = seats,
                StartDate = startDate,
                EndDate = endDate,
                Status = StatusOfRide.Created,
                DriverID = driverID,
                Earnings = 0,                             
            };
        }

        internal decimal GetCharge(int source, int destinaiton)
        {
            return Math.Abs(destinaiton - source) * (decimal)179.68;
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
                Booking ride = SetRide(rider, source, destinaiton, fare*seats, seats, Offer);
                ride = BookingServices.Create(ride);
                BookingServices.Add(ride);              
                BookingServices.SaveData();
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

        internal List<Offer> GetFilteredOffers(int source, int destination,byte seats)
        {
            List<Offer> Offers = new List<Offer>();
            foreach (var Offer in OfferServices.GetAll().FindAll(_=>_.Status== StatusOfRide.Created))
            {
                int MaxSeats = Offer.SeatsAvailable;
                List<int> OfferSequence =new List<int>( Offer.ViaPoints);
                OfferSequence.Insert(0, Offer.Source);
                OfferSequence.Insert(OfferSequence.Count, Offer.Destination);
                if (OfferSequence.IndexOf(Offer.CurrentLocaton) >OfferSequence.IndexOf(Offer.Source))
                {
                    OfferSequence.RemoveRange(OfferSequence.IndexOf(Offer.Source), OfferSequence.IndexOf(Offer.CurrentLocaton));
                }
                    
                if (OfferSequence.IndexOf(source)!=-1 && OfferSequence.IndexOf(source) < OfferSequence.IndexOf(destination))
                {
                    var AssociatedBookings = BookingServices.GetAll().FindAll(_ => _.OfferID == Offer.ID  && _.Status== StatusOfRide.Accepted);
                    bool Flag = false;
                    foreach (int Node in OfferSequence)
                    {
                        if (Node == destination)
                        {
                            if (MaxSeats >= seats)
                            {
                                Offers.Add(Offer);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        foreach (var Element in AssociatedBookings)
                        {
                            if (Node == Element.Source)
                            {
                                MaxSeats -= Element.Seats;                                
                            }
                            else if (Node == Element.Destination)
                            {
                                MaxSeats += Element.Seats;
                            }
                            
                        }
                        if (Node == source)
                        {
                            if (seats > MaxSeats)
                            {
                                break;
                            }
                            else
                            {
                                Flag = true;
                            }
                        }
                        if (Flag && seats > MaxSeats)
                        {
                            break;
                        }
                    }                   
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
                BookingServices.UpdateStatus(Booking_,StatusOfRide.Cancelled);
                Offer.Earnings -=Booking_.Fare;
                BookingServices.SaveData();
                OfferServices.SaveData();
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
            OfferServices.SaveData();
        }

        internal List<Vehicle> GetDriverActiveVehicles(string driverID)
        {
            return VehicleServices.GetAll().FindAll(_ => _.DriverID == driverID && _.IsActive==true);
        }

        internal int GetRidersCount(string offeriD)
        {
            return BookingServices.GetAll().FindAll(_=>_.OfferID==offeriD && _.Status!= StatusOfRide.Cancelled).Count; 
        }

        internal void UpdateBookingData(string offeriD,int currentLocation)
        {
            var Offer_ = GetOfferByID(offeriD);
            var AccociatedBookings=BookingServices.GetAll().FindAll(_=> _.OfferID==offeriD && _.Status!= StatusOfRide.Cancelled);
            List<int> OfferSequence = new List<int>(Offer_.ViaPoints);
            OfferSequence.Insert(0, Offer_.Source);
            OfferSequence.Insert(OfferSequence.Count, Offer_.Destination);
            Offer_.CurrentLocaton = currentLocation;
            AccociatedBookings.ForEach(Element =>
            {
                if (OfferSequence.IndexOf(Element.Destination) <= OfferSequence.IndexOf(Offer_.CurrentLocaton))
                {
                    if(Element.Status== StatusOfRide.Pending)
                {
                    Element.Status = StatusOfRide.Rejected;
                }
                    else
                    {
                        Element.Status = StatusOfRide.Completed;
                    }
                }
            });
            BookingServices.SaveData();
            OfferServices.SaveData();
        }

        internal void EnableVehilce(string vehicleID)
        {
            var Vehicle_ = VehicleServices.GetVehicleByID(vehicleID);
            Vehicle_.IsActive = true;
            VehicleServices.SaveData();
        }

        internal List<Vehicle> GetDriverInActiveVehicles(string driverID)
        {
            return VehicleServices.GetAll().FindAll(_ => _.DriverID == driverID && _.IsActive == false);
        }

        internal bool DisableVehilce(string vehicleiD)
        {
            var Vehicle_ = VehicleServices.GetVehicleByID(vehicleiD);
            var AssociatedOffers = OfferServices.GetAll().FindAll(Element => Element.DriverID == Vehicle_.DriverID);
            
            if (AssociatedOffers.Any(_ => _.Status == StatusOfRide.Created))
            {
                return false;
            }
            else
            {
                Vehicle_.IsActive = false;
                VehicleServices.SaveData();
                return true;
            }
        }

        private Offer GetOfferByID(string offeriD)
        {
            return OfferServices.GetByID(offeriD);
        }
    }
}