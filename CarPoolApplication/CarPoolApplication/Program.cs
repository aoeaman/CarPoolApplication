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
        static void Main(string[] args)
        {
            UtilityService Service = new UtilityService();
            Routine routine = new Routine();
            int SelectedChoice = 0;
            while (SelectedChoice != 3)
            {
                Console.Clear();
                Console.WriteLine("-----Welcome to Pooling Service-----");
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine("1.Driver");
                Console.WriteLine("2.Rider");
                Console.WriteLine("3.Exit");
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
