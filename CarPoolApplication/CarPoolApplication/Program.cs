using System;
using CarPoolApplication.Services;

namespace CarPoolApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            UtilityService Service = new UtilityService();
            Console routine = new Console();
            int SelectedChoice = 0;
            while (SelectedChoice != 3)
            {
                System.Console.Clear();
                System.Console.WriteLine("-----Welcome to Pooling Service-----");
                System.Console.WriteLine("Enter Your Choice");
                System.Console.WriteLine("1.Driver");
                System.Console.WriteLine("2.Rider");
                System.Console.WriteLine("3.Exit");
                SelectedChoice = Service.GetIntegerOnly();
                switch (SelectedChoice)
                {
                    case 1:
                        {
                            routine.DriverPanel();
                            break;
                        }
                    case 2:
                        {
                            routine.RiderPanel();
                            break;
                        }
                }
            }
        }       
    }
}
