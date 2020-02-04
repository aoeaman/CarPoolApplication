using System;
using System.Collections.Generic;
using CarPoolApplication.Models;
using CarPoolApplication.Services;

namespace CarPoolApplication
{
    class Routine
    {
        Operations Operation;
        UtilityService Tools;

        public Routine()
        {
            Operation = new Operations();
            Tools = new UtilityService();
        }

        internal void RiderPanel()
        {
            int SelectedChoice = 0;
            while (SelectedChoice != 3)
            {
                Console.Clear();
                Console.WriteLine("-----Welcome to Rider Panel-----");
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine("1.Sign Up");
                Console.WriteLine("2.Login");
                Console.WriteLine("3.Back");
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
                            Console.WriteLine("Enter UserName");
                            string UserName = Console.ReadLine();
                            Console.WriteLine("Enter Passowrd");
                            string Password = Tools.ReadPassword();
                            Console.WriteLine();
                            Rider rider = Operation.GetRider(UserName,Password);
                            if (rider != null)
                            {
                                RiderConsole(rider);
                            }
                            else
                            {
                                Console.WriteLine("User Details Not Found \nPress any key to continue...");
                                Console.ReadKey();
                            }
                            break;
                        }
                }
            }
        }

        internal void DriverPanel()
        {
            int SelectedChoice = 0;
            while (SelectedChoice != 3)
            {
                Console.Clear();
                Console.WriteLine("-----Welcome to Driver Panel-----");
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine("1.Sign Up");
                Console.WriteLine("2.Login");
                Console.WriteLine("3.Back");
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
                            Console.WriteLine("Enter UserName");
                            string UserName = Console.ReadLine();
                            Console.WriteLine("Enter Passowrd");
                            string Password = Tools.ReadPassword();
                            Console.WriteLine();
                            Driver driver = Operation.GetDriver(UserName,Password);
                            if (driver != null)
                            {
                                DriverConsole(driver);
                            }
                            else
                            {
                                Console.WriteLine("User Details Not Found \nPress any key to continue...");
                                Console.ReadKey();
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
                Console.Clear();
                Console.WriteLine("-----Welcome " + rider.Name + " to Rider Console-----");
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine("1.View All Offers");
                Console.WriteLine("2.Book Offer");
                Console.WriteLine("3.Cancel Offer");
                Console.WriteLine("4.View All Bokings");
                Console.WriteLine("5.Back");
                SelectedChoice = Tools.GetIntegerOnly();
                switch (SelectedChoice)
                {
                    case 1:
                        {
                            List<Offer> Offers = Operation.GetAllAvailableOffers();
                            if (Offers.Count != 0)
                            {
                                Console.WriteLine("ID \t\t\t Source \t Destination \t Seats Available \t Status \t Start Date");
                                Offers.ForEach(Element => Console.WriteLine(Element.ID + " \t\t " + Tools.Cities[Element.Source] + " \t " + Tools.Cities[Element.Destination] + " \t\t " + Element.SeatsAvailable + "\t \t" + Element.Status + " \t\t" + Element.StartDate));
                            }
                            else
                            {
                                Console.WriteLine("No Offers Found");                                                             
                            }
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            for (int i = 0; i < Tools.Cities.Count; i++)
                            {
                                Console.Write(i + 1 + ". " + Tools.Cities[i] + "\t");
                            }
                            Console.WriteLine("\nEnter Source :");
                            int Source = Tools.GetIntegerOnly() - 1;
                            Console.WriteLine("Enter Destination");
                            int Destinaiton = Tools.GetIntegerOnly() - 1;
                            decimal Fare = Operation.GetCharge(Source,Destinaiton);
                            Console.WriteLine("Fare for the ride is :"+Fare);
                            Console.WriteLine("Do you want Continue: Y/N");
                            char Choice = char.Parse(Console.ReadLine());
                            if (Choice=='Y' || Choice=='y')
                            {
                                Console.WriteLine("\nFollowing are the list of Offers for the given Route :");
                                List<Offer> Offers = Operation.GetOffers(Source, Destinaiton);
                                if (Offers.Count != 0)
                                {
                                    Console.WriteLine("ID \t\t\t Source \t Destination \t Seats Available \t Status \t Start Date");
                                    Offers.ForEach(Element => Console.WriteLine(Element.ID + " \t\t " + Tools.Cities[Element.Source] + " \t\t " + Tools.Cities[Element.Destination] + " \t\t " + Element.SeatsAvailable +  "\t \t" + Element.Status + " \t\t" + Element.StartDate));
                                    Console.WriteLine("Enter number of seats to book");
                                    byte Seats = Tools.GetByteOnly();
                                    Console.WriteLine("Enter Offer ID to book Ride");
                                    string OfferID = Console.ReadLine();
                                    bool Status=Operation.BookRide(OfferID, rider,Source,Destinaiton,Fare,Seats);
                                    if (!Status)
                                    {
                                        Console.WriteLine("Cannot book zero or more seats than available seats");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No Offers Found");
                                }
                                Console.ReadKey();
                            }
                            else
                            {
                                break;
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enteryour Booking ID:");
                            string BookingID = Console.ReadLine();
                            if (Operation.RemoveRide(BookingID))
                            {
                                Console.WriteLine("Successfully Cancelled");
                            }
                            else
                            {
                                Console.WriteLine("Can not cancel");
                            }
                            Console.ReadKey();
                            break;
                        }
                    case 4:
                        {
                            List<Booking> Rides= Operation.GetBookings(rider.ID);
                            Console.WriteLine("ID \t\t\t Source \t Destination \t Fare \t\t seats \t\t Status \t");
                            Rides.ForEach(Element => Console.WriteLine(Element.ID + " \t \t " + Tools.Cities[Element.Source] + " \t " + Tools.Cities[Element.Destination] + " \t " + Element.Fare + " \t\t " +Element.Seats+" \t\t "+ Element.Status + " \t\t " +"\n"));
                            Console.ReadKey();
                            break;
                        }
                }
            }  
        }

        void DriverConsole(Driver driver)
        {
            int SelectedChoice = 0;
            while (SelectedChoice != 8)
            {
                Console.Clear();
                Console.WriteLine("-----Welcome "+driver.Name +" to Driver Console-----");
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine("1.Register Vehicle");
                Console.WriteLine("2.Create Offer");
                Console.WriteLine("3.Delete Offer");
                Console.WriteLine("4.Approve Request");
                Console.WriteLine("5.View Created Offer");
                Console.WriteLine("6. Complete Offer");
                Console.WriteLine("7.Update Current Location");
                Console.WriteLine("8.Back");
                SelectedChoice = Tools.GetIntegerOnly();
                switch (SelectedChoice)
                {
                    case 1:
                        {
                            Console.WriteLine("Enter Vehicle Number");
                            string VehicleNumber = Console.ReadLine();
                            Console.WriteLine("Choose Vehicle Type");
                            string[] VehicleTypes= Enum.GetNames(typeof(VehicleType));
                            for(int i=0; i < VehicleTypes.Length; i++)
                            {
                                Console.WriteLine(i + 1 +". "+ VehicleTypes[i]);
                            }
                            int SelectedVehicle = Tools.GetIntegerOnly();
                            VehicleType Type = (VehicleType)Enum.Parse(typeof(VehicleType), (Enum.GetName(typeof(VehicleType),SelectedVehicle - 1)));
                            Console.WriteLine("Enter Vehicle Maker");
                            string Maker = Console.ReadLine();
                            Console.WriteLine("Enter Number of Seats");
                            byte seats = Tools.GetByteOnly();
                            Operation.AddVehicle(driver.ID, VehicleNumber, Maker, Type, seats);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Select Your Vehicle");
                            List<Vehicle> DriverVehicles = Operation.GetDriverVehicles(driver.ID);
                            DriverVehicles.ForEach(Element=>Console.WriteLine(DriverVehicles.IndexOf(Element)+1 +". "+Element.ID+" "+Element.Type));
                            int VehicleID = Tools.GetIntegerOnly() - 1;
                            Console.WriteLine("Following is the list of cities available in Service");
                            for(int i=0;i< Tools.Cities.Count; i++)
                            {
                                Console.Write(i+1 +". " + Tools.Cities[i]+"\t");
                            }
                            Console.WriteLine();
                            Console.WriteLine("Enter Source :");
                            int Source = Tools.GetIntegerOnly() -1;
                            Console.WriteLine("Enter Destination");
                            int Destinaiton = Tools.GetIntegerOnly() -1;                            
                            Console.WriteLine("Enter Via Points:");
                            List<int> ViaPoints = new List<int>();
                            char choice;
                            do
                            {
                                int ViaPoint = Tools.GetIntegerOnly() -1;
                                ViaPoints.Add(ViaPoint);
                                Console.WriteLine("No you wnat to add more Via Points Y/N");
                                choice = Console.ReadKey().KeyChar;
                            } while (choice == 'Y' || choice=='y');
                            Console.WriteLine("\nEnter Seats Available :");
                            byte Seats = Tools.GetByteOnly();
                            Console.WriteLine("Enter Start Date as dd/mm/yy :");
                            string StartDate =Console.ReadLine();
                            Console.WriteLine("Enter End Date as dd/mm/yy :");
                            string EndDate = Console.ReadLine();

                            Operation.AddOffer(DriverVehicles[VehicleID].ID,driver.ID,Source,Destinaiton,ViaPoints,Seats,StartDate,EndDate);

                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter Offer ID");
                            string OfferID = Console.ReadLine();
                            Operation.DeleteOffer(OfferID);
                            break;
                        }
                    case 4:
                        {
                            List<Offer> OffersOfDriver = Operation.GetOfferByDriverID(driver.ID);
                            if (OffersOfDriver.Count != 0)
                            {
                                Console.WriteLine("Select Offer:");
                                OffersOfDriver.ForEach(Element => Console.WriteLine(OffersOfDriver.IndexOf(Element) + 1 + ". " + Element.ID));
                                int Choice = Tools.GetIntegerOnly();
                                List<Booking> Requests=Operation.GetRequests(OffersOfDriver[Choice-1].ID);
                                if (Requests.Count == 0)
                                {
                                    Console.WriteLine("No Requests Found:");
                                    Console.ReadKey();
                                    break;
                                }
                                Console.WriteLine("ID \t Source \t Destination \t Number Of Seats \t Fare");
                                Requests.ForEach(Element => Console.WriteLine(Element.RiderID+" \t "+Tools.Cities[Element.Source] +" \t "+ Tools.Cities[Element.Destination]+" \t "+Element.Seats + " \t " + Element.Fare));
                                Console.WriteLine("Enter ID to Confirm");
                                string ConfirmationID = Console.ReadLine();
                                bool status=Operation.GetBookingConfirmed(OffersOfDriver[Choice - 1].ID, ConfirmationID);
                                if (status)
                                {
                                    Console.WriteLine("Confrimed");
                                }
                                else
                                {
                                    Console.WriteLine("Can Not Confirm Now");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No Offer Found:");
                               
                            }
                            Console.ReadKey();
                            break;
                        }
                    case 5:
                        {
                            List<Offer> Offers= Operation.ViewOffers(driver.ID);
                            if (Offers.Count == 0)
                            {
                                Console.WriteLine("No Offers Created:");
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("ID \t\t Source \t Destination \t Number of Riders \t Status \t Earnings \n");
                            Offers.ForEach(Element => Console.WriteLine(Element.ID+ " \t " + Tools.Cities[Element.Source] + " \t " + Tools.Cities[Element.Destination] + " \t " + Operation.GetRidersCount(Element.ID)+ " \t\t\t " + Element.Status+ " \t " + Element.Earnings+" \n "));
                            Console.ReadKey();
                            break;
                        }
                    case 6:
                        {
                            List<Offer> OffersOfDriver = Operation.GetOfferByDriverID(driver.ID);
                            Console.WriteLine("Enter Offer ID");
                            if (OffersOfDriver.Count != 0)
                            {
                                OffersOfDriver.ForEach(Element => Console.WriteLine(OffersOfDriver.IndexOf(Element) + 1 + ". " + Element.ID));
                                int Choice = Tools.GetIntegerOnly();
                                
                                Operation.CompleteOffer(OffersOfDriver[Choice - 1].ID);
                            }
                            else
                            {
                                Console.WriteLine("No Offer Found:");
                                Console.ReadKey();
                            }
                            break;
                        }
                    case 7:
                        {
                            List<Offer> OffersOfDriver = Operation.GetOfferByDriverID(driver.ID);
                            if (OffersOfDriver.Count != 0)
                            {
                                Console.WriteLine("Select Offer:");
                                OffersOfDriver.ForEach(Element => Console.WriteLine(OffersOfDriver.IndexOf(Element) + 1 + ". " + Element.ID));
                                int Choice = Tools.GetIntegerOnly()-1;
                                List<int> OfferSequence = new List<int>(OffersOfDriver[Choice].ViaPoints);
                                OfferSequence.Insert(0, OffersOfDriver[Choice].Source);
                                OfferSequence.Insert(OfferSequence.Count, OffersOfDriver[Choice].Destination);
                                Console.WriteLine("Select Current Location:");
                                if (OfferSequence[OffersOfDriver[Choice].CurrentLocaton] > OfferSequence[OffersOfDriver[Choice].Source])
                                {
                                    OfferSequence.RemoveRange(OfferSequence[OffersOfDriver[Choice].Source], OfferSequence[OffersOfDriver[Choice].CurrentLocaton]);
                                }
                                OfferSequence.ForEach(_ => Console.WriteLine(OfferSequence.IndexOf(_)+1 +". "+Tools.Cities[_]));
                                int CurrentLoaction = Tools.GetIntegerOnly() - 1;
                                OffersOfDriver[Choice].CurrentLocaton = OfferSequence[CurrentLoaction];
                                Operation.UpdateBookingData(OffersOfDriver[Choice].ID, OfferSequence[CurrentLoaction]);
                                Console.WriteLine("Successfully Updated");
                            }
                            else
                            {
                                Console.WriteLine("No Offer Found:");
                            }
                            Console.ReadKey();
                            break;
                        }

                }
            }
        }

        void RiderSignUp()
        {
            try
            {
                Console.WriteLine("Enter Name");
                string Name = Console.ReadLine();
                Console.WriteLine("Enter UserName");
                string UserName = Console.ReadLine();
                Console.WriteLine("Age");
                byte Age = Tools.GetByteOnly();
                Console.WriteLine("Gender M/F");
                char Gender = char.Parse(Console.ReadLine());
                Console.WriteLine("Enter Phone Number");
                string PhoneNumber = Console.ReadLine();
                Console.WriteLine("Create Passowrd");
                string Password = Tools.ReadPassword();
                Operation.AddRider(Name, UserName, Age, Gender, PhoneNumber, Password);
            }
            catch (Exception)
            {
                Console.WriteLine("Something Occured OR UserName Already Exists\n press any key to continue");
            }
        }

        void DriverSignUp()
        {
            try
            {
                Console.WriteLine("Enter Name");
                string Name = Console.ReadLine();
                Console.WriteLine("Enter UserName");
                string UserName = Console.ReadLine();
                Console.WriteLine("Age");
                byte Age = Tools.GetByteOnly();
                Console.WriteLine("Gender M/F");
                char Gender = char.Parse(Console.ReadLine());
                Console.WriteLine("Enter Phone Number");
                string PhoneNumber = Console.ReadLine();
                Console.WriteLine("Create Passowrd");
                string Password = Tools.ReadPassword();
                Console.WriteLine("\nEnter Driving Liscence Number");
                string DrivingLiscenceNumber = Console.ReadLine();

                Operation.AddDriver(Name, UserName, Age, Gender, PhoneNumber, Password, DrivingLiscenceNumber);
            }
            catch (Exception)
            {
                Console.WriteLine("Something Occured OR UserName Already Exists\n press any key to continue");
                Console.ReadKey();
            }
            
        }
    }
}
