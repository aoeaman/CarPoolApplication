using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPoolApplication.Models;
using CarPoolApplication.Services;

namespace CarPoolApplication
{
    class Routine
    {
        Operations Operation;
        UtilityService Service;

        public Routine()
        {
            Operation = new Operations();
            Service = new UtilityService();
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
                SelectedChoice = Service.GetIntegerOnly();
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
                            Console.WriteLine("Create Passowrd");
                            string Password = Service.ReadPassword();
                            Console.WriteLine();
                            Rider rider = Operation.Data.Riders.Find(Element => Element.Username == UserName && Element.Password == Password);
                            if (rider != null)
                            {

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
                SelectedChoice = Service.GetIntegerOnly();
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
                            Console.WriteLine("Create Passowrd");
                            string Password = Service.ReadPassword();
                            Console.WriteLine();
                            Driver driver = Operation.Data.Drivers.Find(Element => Element.Username == UserName && Element.Password == Password);
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
                Console.WriteLine("1.View Offer");
                Console.WriteLine("2.Book Offer");
                Console.WriteLine("3.Cancel Ride");
                Console.WriteLine("4.View All Bokings");
                Console.WriteLine("5.Back");
                SelectedChoice = Service.GetIntegerOnly();
                switch (SelectedChoice)
                {
                    case 1:
                        {

                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                }
            }  
        }

        void DriverConsole(Driver driver)
        {
            int SelectedChoice = 0;
            while (SelectedChoice != 6)
            {
                Console.Clear();
                Console.WriteLine("-----Welcome "+driver.Name +" to Rider Console-----");
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine("1.Register Vehicle");
                Console.WriteLine("2.Create Offer");
                Console.WriteLine("3.Delete Offer");
                Console.WriteLine("4.Approve Request");
                Console.WriteLine("5.View Created Offer");
                Console.WriteLine("6.Back");
                SelectedChoice = Service.GetIntegerOnly();
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
                            int SelectedVehicle = Service.GetIntegerOnly();
                            VehicleType Type = (VehicleType)Enum.Parse(typeof(VehicleType), (Enum.GetName(typeof(VehicleType),SelectedVehicle - 1)));
                            Console.WriteLine("Enter Vehicle Maker");
                            string Maker = Console.ReadLine();
                            Console.WriteLine("Enter Number of Seats");
                            byte seats = Service.GetByteOnly();
                            Operation.AddVehicle(driver, VehicleNumber, Maker, Type, seats);
                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter Offer ID");
                            string OfferID = Console.ReadLine();
                            Operation.DeleteOffer(driver,OfferID);
                            break;
                        }
                    case 4:
                        {
                            List<Trip> TripsOfDriver = Operation.ShowRequests(driver);
                            if (TripsOfDriver != null)
                            {
                                TripsOfDriver.ForEach(Element => Console.WriteLine(TripsOfDriver.IndexOf(Element) + 1 + ". " + Element.ID));
                                int Choice = Service.GetIntegerOnly();
                                List<Ride> Requests=Operation.GetRequests(TripsOfDriver[Choice-1]);
                                Console.WriteLine("Name \t Source \t Destination \t Number Of Seats");
                                Requests.ForEach(Element => Console.WriteLine(Element.RiderID+" \t "+Element.Source +" \t "+ Element.Destination+" \t "+Element.Seats));
                                Console.WriteLine("Enter ID to Confirm");
                                string ConfirmationID = Console.ReadLine();
                                Operation.GetBookingConfirmed(TripsOfDriver[Choice - 1], ConfirmationID);
                            }
                            else
                            {
                                Console.WriteLine("No Requests Found:");
                                Console.ReadKey();
                            }
                            break;
                        }
                    case 5:
                        {
                            List<Trip> trips= Operation.ViewOffers(driver);
                            Console.WriteLine("ID \t Source \t Destination \t Number of Riders \t Earnings");
                            trips.ForEach(Element => Console.WriteLine(Element.ID+" \t "+ Element.Source +" \t "+ Element.Destination +" \t "+ Element.Bookings.Count+ " \t "+ Element.Earnings));
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
                byte Age = Service.GetByteOnly();
                Console.WriteLine("Gender M/F");
                char Gender = char.Parse(Console.ReadLine());
                Console.WriteLine("Enter Phone Number");
                string PhoneNumber = Console.ReadLine();
                Console.WriteLine("Create Passowrd");
                string Password = Service.ReadPassword();
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
                byte Age = Service.GetByteOnly();
                Console.WriteLine("Gender M/F");
                char Gender = char.Parse(Console.ReadLine());
                Console.WriteLine("Enter Phone Number");
                string PhoneNumber = Console.ReadLine();
                Console.WriteLine("Create Passowrd");
                string Password = Service.ReadPassword();
                Console.WriteLine("\nEnter Driving Liscence Number");
                string DrivingLiscenceNumber = Console.ReadLine();

                Operation.AddDriver(Name, UserName, Age, Gender, PhoneNumber, Password, DrivingLiscenceNumber);
            }
            catch (Exception)
            {
                Console.WriteLine("Something Occured OR UserName Already Exists\n press any key to continue");
            }
        }

        void CreateVehicle()
        {

        }
    }
}
