using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPoolApplication.Services;
using CarPoolApplication.Models;

namespace CarPoolApplication
{
    class Program
    {
        Operations Operation;
        UtilityService Service;
        public Program()
        {
            Operation = new Operations();
            Service = new UtilityService();
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            int SelectedChoice = 0;
            while (SelectedChoice != 3)
            {
                Console.Clear();
                Console.WriteLine("-----Welcome to Pooling Service-----");
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine("1.Driver");
                Console.WriteLine("2.Rider");
                Console.WriteLine("3.Exit");
                SelectedChoice = program.Service.GetIntegerOnly();
                switch (SelectedChoice)
                {
                    case 1:
                        {
                            program.Driver();
                            break;
                        }
                    case 2:
                        {
                            program.Rider();
                            break;
                        }
                }
            }
        }

        void Rider()
        {
            int SelectedChoice = 0;
            Console.WriteLine("-----Welcome to Rider Console-----");
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
                        if (Login())
                        {

                        }
                        else
                        {

                        }
                        break;
                    }
            }
        }


        void Driver()
        {
            int SelectedChoice = 0;
            Console.WriteLine("-----Welcome to Driver Console-----");
            Console.WriteLine("Enter Your Choice");
            Console.WriteLine("1.Sign Up");
            Console.WriteLine("2.Login");
            Console.WriteLine("3.Back");
            SelectedChoice = Service.GetIntegerOnly();
            switch (SelectedChoice)
            {
                case 1:
                    {

                        break;
                    }
                case 2:
                    {
                        if (Login())
                        {

                        }
                        else
                        {

                        }
                        break;
                    }
            }
        }

        bool Login()
        {

            return true;
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
                char Gender = Console.ReadKey().KeyChar;
                Console.ReadKey();
                Console.WriteLine("Enter Phone Number");
                string PhoneNumber = Console.ReadLine();
                Console.WriteLine("Create Passowrd");
                string Password = Service.ReadPassword();
                Console.WriteLine("\nEnter Driving Liscence Number");
                string DrivingLiscenceNumber = Console.ReadLine();               
            }
            catch (Exception)
            {
                Console.WriteLine("Error Occured........\n press any key to continue");
            }
        }

        void CreateVehicle()
        {
            
        }
    }
}
