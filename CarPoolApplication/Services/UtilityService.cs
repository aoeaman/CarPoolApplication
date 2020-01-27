using System;
using System.Text;

namespace CarPoolApplication.Services
{
    public class UtilityService
    {
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

        public string GenerateID(string name)
        {
            return name.Substring(0, 3).ToUpper() + DateTime.UtcNow.Year + DateTime.UtcNow.Millisecond;
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
