using System.Globalization;
using System.Text.RegularExpressions;

namespace NationalEducation
{
    internal static class InputValidator
    {
        public static string GetAndValidNameInput(string messageForUser)
        {
            bool isValid = false;
            string userInput = "";
            // Ne laisse pas passer les accents
            string pattern = ConstantValue.NAME_PATTERN;

            while (!isValid)
            {
                Console.Write(messageForUser);
                userInput = Console.ReadLine();
                if (Regex.IsMatch(userInput, pattern))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine(ConstantValue.NAME_ERROR_MESSAGE);
                }
            }

            return userInput;
        }

        public static DateTime GetAndValidDateInput(string messageForUser)
        {
            bool isValid = false;
            string userInput;
            DateTime date = DateTime.Now;

            while (!isValid)
            {
                Console.Write(messageForUser);
                userInput = Console.ReadLine();
                if (DateTime.TryParseExact(userInput, ConstantValue.DATE_FORMAT, null, DateTimeStyles.None, out date))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine(ConstantValue.DATE_ERROR_MESSAGE);
                }
            }

            return date;
        }
    }
}
