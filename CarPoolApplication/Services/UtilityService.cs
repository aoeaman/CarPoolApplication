using System;
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
    }
}
