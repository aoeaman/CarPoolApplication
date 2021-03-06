﻿using System;
using System.Collections.Generic;
using CarPoolApplication.Models;
using CarPoolApplication.Services;

namespace CarPoolApplication
{
    class Console
    {
        Operations Operation;
        UtilityService Tools;

        public Console()
        {
            Operation = new Operations();
            Tools = new UtilityService();
        }

        internal void RiderPanel()
        {
            int SelectedChoice = 0;
            while (SelectedChoice != 4)
            {
                System.Console.Clear();
                System.Console.WriteLine("-----Welcome to Rider Panel-----");
                System.Console.WriteLine("Enter Your Choice");
                System.Console.WriteLine("1.Sign Up");
                System.Console.WriteLine("2.Login");
                System.Console.WriteLine("3.Forgot Password");
                System.Console.WriteLine("4.Back");
                SelectedChoice = Tools.GetIntegerOnly();
                switch (SelectedChoice)
                {
                    case 1:
                        {
                            RiderSignUp();
                            break;
                        }
                    case 2:
                        {
                            System.Console.WriteLine("Enter UserName");
                            string UserName = System.Console.ReadLine();
                            System.Console.WriteLine("Enter Passowrd");
                            string Password = Tools.ReadPassword();
                            System.Console.WriteLine();
                            Rider rider = Operation.GetRider(UserName,Password);
                            if (rider != null)
                            {
                                RiderConsole(rider);
                            }
                            else
                            {
                                System.Console.WriteLine("User Details Not Found \nPress any key to continue...");
                                System.Console.ReadKey();
                            }
                            break;
                        }
                    case 3:
                        {
                            System.Console.WriteLine("Enter UserName");
                            string UserName = System.Console.ReadLine();
                            System.Console.WriteLine("Enter Phone Number");
                            string PhoneNumber = System.Console.ReadLine();
                            var User= Operation.PasswordHelper<Rider>(UserName,PhoneNumber);
                            if (User != null)
                            {
                                System.Console.WriteLine("Hello "+((Rider)User).Name+"\nEnter New Passowrd");
                                string Password = Tools.ReadPassword();
                                Operation.SetNewPassword((Rider)User,Password);
                            }
                            else
                            {
                                System.Console.WriteLine("User Details Not Found \nPress any key to continue...");
                                System.Console.ReadKey();
                            }
                            break;
                        }
                }
            }
        }

        internal void DriverPanel()
        {
            int SelectedChoice = 0;
            while (SelectedChoice != 4)
            {
                System.Console.Clear();
                System.Console.WriteLine("-----Welcome to Driver Panel-----");
                System.Console.WriteLine("Enter Your Choice");
                System.Console.WriteLine("1.Sign Up");
                System.Console.WriteLine("2.Login");
                System.Console.WriteLine("3.Forgot Password");
                System.Console.WriteLine("4.Back");
                SelectedChoice = Tools.GetIntegerOnly();
                switch (SelectedChoice)
                {
                    case 1:
                        {
                            DriverSignUp();
                            break;
                        }
                    case 2:
                        {
                            System.Console.WriteLine("Enter UserName");
                            string UserName = System.Console.ReadLine();
                            System.Console.WriteLine("Enter Passowrd");
                            string Password = Tools.ReadPassword();
                            System.Console.WriteLine();
                            Driver driver = Operation.GetDriver(UserName,Password);
                            if (driver != null)
                            {
                                DriverConsole(driver);
                            }
                            else
                            {
                                System.Console.WriteLine("User Details Not Found \nPress any key to continue...");
                                System.Console.ReadKey();
                            }
                            break;
                        }
                    case 3:
                        {
                            System.Console.WriteLine("Enter UserName");
                            string UserName = System.Console.ReadLine();
                            System.Console.WriteLine("Enter Phone Number");
                            string PhoneNumber = System.Console.ReadLine();
                            var User = Operation.PasswordHelper<Driver>(UserName,PhoneNumber);
                            if (User != null)
                            {
                                System.Console.WriteLine("Hello "+((Driver)User).Name+"\nEnter New Passowrd");
                                string Password = Tools.ReadPassword();
                                Operation.SetNewPassword((Driver)User,Password);
                            }
                            else
                            {
                                System.Console.WriteLine("User Details Not Found \nPress any key to continue...");
                                System.Console.ReadKey();
                            }
                            break;
                        }
                }
            }
        }

        void RiderConsole(Rider rider)
        {
            int SelectedChoice = 0;
            while (SelectedChoice != 5)
            {
                System.Console.Clear();
                System.Console.WriteLine("-----Welcome " + rider.Name + " to Rider Console-----");
                System.Console.WriteLine("Enter Your Choice");
                System.Console.WriteLine("1.View All Offers");
                System.Console.WriteLine("2.Book Offer");
                System.Console.WriteLine("3.Cancel Offer");
                System.Console.WriteLine("4.View All Bokings");
                System.Console.WriteLine("5.Back");
                SelectedChoice = Tools.GetIntegerOnly();
                switch (SelectedChoice)
                {
                    case 1:
                        {
                            List<Offer> Offers = Operation.GetAllAvailableOffers();
                            if (Offers.Count != 0)
                            {
                                System.Console.WriteLine("ID \t\t\t Source \t Destination \t Seats Available \t Status \t Start Date");
                                Offers.ForEach(Element => System.Console.WriteLine(Element.ID + " \t\t " + Tools.Cities[Element.Source] + " \t " + Tools.Cities[Element.Destination] + " \t\t " + Element.SeatsAvailable + "\t \t" + Element.Status + " \t\t" + Element.StartDate));
                            }
                            else
                            {
                                System.Console.WriteLine("No Offers Found");                                                             
                            }
                            System.Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            for (int i = 0; i < Tools.Cities.Count; i++)
                            {
                                System.Console.Write(i + 1 + ". " + Tools.Cities[i] + "\t");
                            }
                            System.Console.WriteLine("\nEnter Source :");
                            int Source = Tools.GetIntegerOnly() - 1;
                            System.Console.WriteLine("Enter Destination");
                            int Destinaiton = Tools.GetIntegerOnly() - 1;
                            System.Console.WriteLine("Enter number of seats to book");
                            byte Seats = Tools.GetByteOnly();
                            decimal Fare = Operation.GetCharge(Source,Destinaiton)*Seats;
                            System.Console.WriteLine("Fare for the ride is :"+ Fare);
                            System.Console.WriteLine("Do you want Continue: Y/N");
                            char Choice = char.Parse(System.Console.ReadLine());
                            if (Choice=='Y' || Choice=='y')
                            {
                                System.Console.WriteLine("\nFollowing are the list of Offers for the given Route :");
                                List<Offer> Offers = Operation.GetFilteredOffers(Source, Destinaiton,Seats);
                                if (Offers.Count != 0)
                                {
                                    System.Console.WriteLine("ID \t\t\t Source \t Destination \t Status \t Start Date");
                                    Offers.ForEach(Element => System.Console.WriteLine(Element.ID + " \t\t " + Tools.Cities[Element.Source] + " \t\t " + Tools.Cities[Element.Destination] + " \t\t " + Element.Status + " \t\t" + Element.StartDate));
                                    System.Console.WriteLine("Enter Offer ID to book Ride");
                                    string OfferID = System.Console.ReadLine();
                                    bool Status=Operation.BookRide(OfferID, rider,Source,Destinaiton,Fare,Seats);
                                    if (!Status)
                                    {
                                        System.Console.WriteLine("Invalid offer ID");
                                    }
                                }
                                else
                                {
                                    System.Console.WriteLine("No Offers Found");
                                }
                                System.Console.ReadKey();
                            }
                            else
                            {
                                break;
                            }
                            break;
                        }
                    case 3:
                        {
                            System.Console.WriteLine("Enteryour Booking ID:");
                            string BookingID = System.Console.ReadLine();
                            if (Operation.RemoveRide(BookingID))
                            {
                                System.Console.WriteLine("Successfully Cancelled");
                            }
                            else
                            {
                                System.Console.WriteLine("Can not cancel");
                            }
                            System.Console.ReadKey();
                            break;
                        }
                    case 4:
                        {
                            List<Booking> Rides= Operation.GetBookings(rider.ID);
                            System.Console.WriteLine("ID \t\t\t Source \t Destination \t Fare \t\t seats \t\t Status \t");
                            Rides.ForEach(Element => System.Console.WriteLine(Element.ID + " \t \t " + Tools.Cities[Element.Source] + " \t " + Tools.Cities[Element.Destination] + " \t " + Element.Fare + " \t\t " + Element.Seats+" \t\t "+ Element.Status + " \t\t " +"\n"));
                            System.Console.ReadKey();
                            break;
                        }
                }
            }  
        }

        void DriverConsole(Driver driver)
        {
            int SelectedChoice = 0;
            while (SelectedChoice != 9)
            {
                System.Console.Clear();
                System.Console.WriteLine("-----Welcome "+ driver.Name +" to Driver Console-----");
                System.Console.WriteLine("Enter Your Choice");
                System.Console.WriteLine("1.Register Vehicle");
                System.Console.WriteLine("2.Enable/Disable Vehicle");
                System.Console.WriteLine("3.Create Offer");
                System.Console.WriteLine("4.Delete Offer");
                System.Console.WriteLine("5.Approve Request");
                System.Console.WriteLine("6.View Created Offer");
                System.Console.WriteLine("7.Complete Offer");
                System.Console.WriteLine("8.Update Current Location");
                System.Console.WriteLine("9.Back");
                SelectedChoice = Tools.GetIntegerOnly();
                switch (SelectedChoice)
                {
                    case 1:
                        {
                            System.Console.WriteLine("Enter Vehicle Number");
                            string VehicleNumber = System.Console.ReadLine();
                            System.Console.WriteLine("Choose Vehicle Type");
                            string[] VehicleTypes= Enum.GetNames(typeof(VehicleType));
                            for(int i=0; i < VehicleTypes.Length; i++)
                            {
                                System.Console.WriteLine(i + 1 +". "+ VehicleTypes[i]);
                            }
                            int SelectedVehicle = Tools.GetIntegerOnly();
                            VehicleType Type = (VehicleType)Enum.Parse(typeof(VehicleType), (Enum.GetName(typeof(VehicleType),SelectedVehicle - 1)));
                            System.Console.WriteLine("Enter Vehicle Maker");
                            string Maker = System.Console.ReadLine();
                            System.Console.WriteLine("Enter Number of Seats");
                            byte seats = Tools.GetByteOnly();
                            Operation.AddVehicle(driver.ID, VehicleNumber, Maker, Type, seats);
                            break;
                        }
                    case 2:
                        {
                            System.Console.WriteLine("1. Enable Vehicle");
                            System.Console.WriteLine("2. Disable Vehicle");
                            int Choice = Tools.GetIntegerOnly();
                            switch (Choice)
                            {
                                case 1:
                                    {
                                        List<Vehicle> DriverVehicles = Operation.GetDriverInActiveVehicles(driver.ID);
                                        if (DriverVehicles.Count == 0)
                                        {
                                            break;
                                        }
                                        DriverVehicles.ForEach(Element => System.Console.WriteLine(DriverVehicles.IndexOf(Element) + 1 + ". " + Element.ID + " " + Element.Type));
                                        int VehicleID = Tools.GetIntegerOnly() - 1;
                                        Operation.EnableVehilce(DriverVehicles[VehicleID].ID);
                                        break;
                                    }
                                case 2:
                                    {
                                        List<Vehicle> DriverVehicles = Operation.GetDriverActiveVehicles(driver.ID);
                                        DriverVehicles.ForEach(Element => System.Console.WriteLine(DriverVehicles.IndexOf(Element) + 1 + ". " + Element.ID + " " + Element.Type));
                                        int VehicleID = Tools.GetIntegerOnly() - 1;
                                        if (!Operation.DisableVehilce(DriverVehicles[VehicleID].ID))
                                        {
                                            System.Console.WriteLine("Cannot Disable at this Moment as an offer is ongoing");
                                            System.Console.ReadKey();
                                        }
                                        break;
                                    }
                            }
                            
                            break;
                        }
                    case 3:
                        {
                            System.Console.WriteLine("Select Your Vehicle");
                            List<Vehicle> DriverVehicles = Operation.GetDriverActiveVehicles(driver.ID);
                            DriverVehicles.ForEach(Element=> System.Console.WriteLine(DriverVehicles.IndexOf(Element) +1 +". "+ Element.ID+" "+ Element.Type));
                            int VehicleID = Tools.GetIntegerOnly() - 1;
                            System.Console.WriteLine("Following is the list of cities available in Service");
                            for(int i=0;i< Tools.Cities.Count; i++)
                            {
                                System.Console.Write(i + 1 +". " + Tools.Cities[i] +"\t");
                            }
                            System.Console.WriteLine();
                            System.Console.WriteLine("Enter Source :");
                            int Source = Tools.GetIntegerOnly() -1;
                            System.Console.WriteLine("Enter Destination");
                            int Destinaiton = Tools.GetIntegerOnly() -1;
                            System.Console.WriteLine("Enter Via Points:");
                            List<int> ViaPoints = new List<int>();
                            char choice;
                            System.Console.WriteLine("Do you want to add Via Points Y/N");
                            choice = Tools.GetCharOnly();
                            while (choice == 'Y' || choice == 'y') 
                            {
                                System.Console.WriteLine("Enter via point");
                                int ViaPoint = Tools.GetIntegerOnly() -1;
                                ViaPoints.Add(ViaPoint);
                                System.Console.WriteLine("Do you want to add more Via Points Y/N");
                                choice = System.Console.ReadKey().KeyChar;
                            }
                            System.Console.WriteLine("\nEnter Seats Available :");
                            byte Seats = Tools.GetByteOnly();
                            System.Console.WriteLine("Enter Start Date as dd/MM/yyyy HH:mm :");
                            DateTime StartDate = Tools.GetDateTimeonly();
                            System.Console.WriteLine("Enter End Date as dd/mm/yy :");
                            DateTime EndDate = Tools.GetDateTimeonly();

                            Operation.AddOffer(DriverVehicles[VehicleID].ID,driver.ID,Source,Destinaiton,ViaPoints,Seats,StartDate,EndDate);

                            break;
                        }
                    case 4:
                        {
                            System.Console.WriteLine("Enter Offer ID");
                            string OfferID = System.Console.ReadLine();
                            Operation.DeleteOffer(OfferID);
                            break;
                        }
                    case 5:
                        {
                            List<Offer> OffersOfDriver = Operation.GetOfferByDriverID(driver.ID);
                            if (OffersOfDriver.Count != 0)
                            {
                                System.Console.WriteLine("Select Offer:");
                                OffersOfDriver.ForEach(Element => System.Console.WriteLine(OffersOfDriver.IndexOf(Element) + 1 + ". " + Element.ID));
                                int Choice = Tools.GetIntegerOnly();
                                List<Booking> Requests=Operation.GetRequests(OffersOfDriver[Choice-1].ID);
                                if (Requests.Count == 0)
                                {
                                    System.Console.WriteLine("No Requests Found:");
                                    System.Console.ReadKey();
                                    break;
                                }
                                System.Console.WriteLine("ID \t Source \t Destination \t Number Of Seats \t Fare");
                                Requests.ForEach(Element => System.Console.WriteLine(Element.ID+" \t "+ Tools.Cities[Element.Source] +" \t "+ Tools.Cities[Element.Destination]+" \t "+ Element.Seats + " \t " + Element.Fare));
                                System.Console.WriteLine("Enter ID to Confirm");
                                string ConfirmationID = System.Console.ReadLine();
                                Operation.GetBookingConfirmed(OffersOfDriver[Choice - 1].ID, ConfirmationID);                           
                            }
                            else
                            {
                                System.Console.WriteLine("No Offer Found:");
                               
                            }
                            System.Console.ReadKey();
                            break;
                        }
                    case 6:
                        {
                            List<Offer> Offers= Operation.ViewOffers(driver.ID);
                            if (Offers.Count == 0)
                            {
                                System.Console.WriteLine("No Offers Created:");
                                System.Console.ReadKey();
                                break;
                            }
                            System.Console.WriteLine("ID \t\t Source \t Destination \t Number of Riders \t Status \t Earnings \n");
                            Offers.ForEach(Element => System.Console.WriteLine(Element.ID+ " \t " + Tools.Cities[Element.Source] + " \t " + Tools.Cities[Element.Destination] + " \t " + Operation.GetRidersCount(Element.ID)+ " \t\t\t " + Element.Status+ " \t " + Element.Earnings+" \n "));
                            System.Console.ReadKey();
                            break;
                        }
                    case 7:
                        {
                            List<Offer> OffersOfDriver = Operation.GetOfferByDriverID(driver.ID);
                            System.Console.WriteLine("Enter Offer ID");
                            if (OffersOfDriver.Count != 0)
                            {
                                OffersOfDriver.ForEach(Element => System.Console.WriteLine(OffersOfDriver.IndexOf(Element) + 1 + ". " + Element.ID));
                                int Choice = Tools.GetIntegerOnly();
                                
                                Operation.CompleteOffer(OffersOfDriver[Choice - 1].ID);
                            }
                            else
                            {
                                System.Console.WriteLine("No Offer Found:");
                                System.Console.ReadKey();
                            }
                            break;
                        }
                    case 8:
                        {
                            List<Offer> OffersOfDriver = Operation.GetOfferByDriverID(driver.ID);
                            if (OffersOfDriver.Count != 0)
                            {
                                System.Console.WriteLine("Select Offer:");
                                OffersOfDriver.ForEach(Element => System.Console.WriteLine(OffersOfDriver.IndexOf(Element) + 1 + ". " + Element.ID));
                                int Choice = Tools.GetIntegerOnly()-1;
                                List<int> OfferSequence = new List<int>(OffersOfDriver[Choice].ViaPoints);
                                OfferSequence.Insert(0, OffersOfDriver[Choice].Source);
                                OfferSequence.Insert(OfferSequence.Count, OffersOfDriver[Choice].Destination);
                                System.Console.WriteLine("Select Current Location:");
                                if (OfferSequence.IndexOf(OffersOfDriver[Choice].CurrentLocaton) > OfferSequence.IndexOf(OffersOfDriver[Choice].Source))
                                {
                                    OfferSequence.RemoveRange(OfferSequence.IndexOf(OffersOfDriver[Choice].Source), OfferSequence.IndexOf(OffersOfDriver[Choice].CurrentLocaton));
                                }
                                OfferSequence.ForEach(_ => System.Console.WriteLine(OfferSequence.IndexOf(_) +1 +". "+ Tools.Cities[_]));
                                int CurrentLoaction = Tools.GetIntegerOnly() - 1;
                                OffersOfDriver[Choice].CurrentLocaton = OfferSequence[CurrentLoaction];
                                Operation.UpdateBookingData(OffersOfDriver[Choice].ID, OfferSequence[CurrentLoaction]);
                                System.Console.WriteLine("Successfully Updated");
                            }
                            else
                            {
                                System.Console.WriteLine("No Offer Found:");
                            }
                            System.Console.ReadKey();
                            break;
                        }

                }
            }
        }

        void RiderSignUp()
        {
            try
            {
                System.Console.WriteLine("Enter Name");
                string Name = System.Console.ReadLine();
                System.Console.WriteLine("Enter UserName");
                string UserName = System.Console.ReadLine();
                System.Console.WriteLine("Age");
                byte Age = Tools.GetByteOnly();
                System.Console.WriteLine("Gender M/F");
                char Gender = char.Parse(System.Console.ReadLine());
                System.Console.WriteLine("Enter Phone Number");
                string PhoneNumber = System.Console.ReadLine();
                System.Console.WriteLine("Create Passowrd");
                string Password = Tools.ReadPassword();
                Operation.AddRider(Name, UserName, Age, Gender, PhoneNumber, Password);
            }
            catch (Exception)
            {
                System.Console.WriteLine("Something Occured OR UserName Already Exists\n press any key to continue");
            }
        }

        void DriverSignUp()
        {
            try
            {
                System.Console.WriteLine("Enter Name");
                string Name = System.Console.ReadLine();
                System.Console.WriteLine("Enter UserName");
                string UserName = System.Console.ReadLine();
                System.Console.WriteLine("Age");
                byte Age = Tools.GetByteOnly();
                System.Console.WriteLine("Gender M/F");
                char Gender = char.Parse(System.Console.ReadLine());
                System.Console.WriteLine("Enter Phone Number");
                string PhoneNumber = System.Console.ReadLine();
                System.Console.WriteLine("Create Passowrd");
                string Password = Tools.ReadPassword();
                System.Console.WriteLine("\nEnter Driving Liscence Number");
                string DrivingLiscenceNumber = System.Console.ReadLine();

                Operation.AddDriver(Name, UserName, Age, Gender, PhoneNumber, Password, DrivingLiscenceNumber);
            }
            catch (Exception)
            {
                System.Console.WriteLine("Something Occured OR UserName Already Exists\n press any key to continue");
                System.Console.ReadKey();
            }
            
        }
    }
}
