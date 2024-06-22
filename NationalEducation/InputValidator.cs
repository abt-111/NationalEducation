using System.Globalization;
using System.Text.RegularExpressions;

namespace NationalEducation
{
    internal static class InputValidator
    {
        public static int GetAndValidIndexInput(string messageForUser, int maxIndex)
        {
            bool isValid = false;
            string userInput = "";
            int index = 0;

            while (!isValid)
            {
                Console.Write(messageForUser);
                userInput = Console.ReadLine();
                if (Int32.TryParse(userInput, out index))
                {
                    if (index >= 0 && index < maxIndex)
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine($"Vous devez entrer un entier compris entre 0 et {maxIndex - 1}.\n");
                    }
                }
                else
                {
                    Console.WriteLine($"Vous devez entrer un entier compris entre 0 et {maxIndex - 1}.\n");
                }
            }

            return index;
        }

        public static float GetAndValidGradeInput(string messageForUser)
        {
            bool isValid = false;
            string userInput = "";
            float gradeValue = 0.0f;

            while (!isValid)
            {
                Console.Write(messageForUser);
                userInput = Console.ReadLine();
                if (Single.TryParse(userInput, out gradeValue))
                {
                    if (gradeValue >= 0 && gradeValue <= ConstantValue.MAX_GRADE)
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine($"Vous devez entrer un réel compris entre 0 et {ConstantValue.MAX_GRADE}.\n");
                    }
                }
                else
                {
                    Console.WriteLine($"Vous devez entrer un réel compris entre 0 et {ConstantValue.MAX_GRADE}.\n");
                }
            }

            return gradeValue;
        }

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
