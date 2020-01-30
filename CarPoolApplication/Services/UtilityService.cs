using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Services
{
    public class UtilityService
    {
        public string GenerateID()
        {
            Random random = new Random();
            return random.Next(10000,99999).ToString()+ DateTime.UtcNow.Year + DateTime.UtcNow.Millisecond;
        }

        public List<string> Cities = new List<string>() { "Banglore", "Chandigarh", "Dehradun", "Gwalior", "Hyderabad", "Mumbai", "Vizag" };

        public byte GetByteOnly()
        {
            byte Choice;
            try
            {
                Choice = byte.Parse(Console.ReadLine());
                return Choice;
            }
            catch (Exception)
            {
                return GetByteOnly();
            }
        }

        public int GetIntegerOnly()
        {
            int Choice;
            try
            {
                Choice = int.Parse(Console.ReadLine());
                return Choice;
            }
            catch (Exception)
            {
                return GetIntegerOnly();
            }
        }

        public string ReadPassword()
        {
            var result = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        return result.ToString();
                    case ConsoleKey.Backspace:
                        if (result.Length == 0)
                        {
                            continue;
                        }
                        result.Length--;
                        Console.Write("\b \b");
                        continue;
                    default:
                        result.Append(key.KeyChar);
                        Console.Write("*");
                        continue;
                }
            }
        }

        public partial class Path
        {
            public readonly string Driver ="C:\\repos\\CarPoolApplication\\CarPoolApplication\\Driver.JSON";
            public readonly string Rider ="C:\\repos\\CarPoolApplication\\CarPoolApplication\\Rider.JSON";
            public readonly string Vehicle ="C:\\repos\\CarPoolApplication\\CarPoolApplication\\Vehicle.JSON";
            public readonly string Trip = "C:\\repos\\CarPoolApplication\\CarPoolApplication\\Trips.JSON";
        }

        public char GetCharOnly()
        {
            char Choice;
            try
            {
                Choice = char.Parse(Console.ReadLine());
                return Choice;
            }
            catch (Exception)
            {
                return GetCharOnly();
            }
        }
    }
}
