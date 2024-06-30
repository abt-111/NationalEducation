using System.Dynamic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NationalEducation
{
    internal static class InputValidator
    {
        public static int GetAndValidIndexInput(string indicationForUser, int maxIndex)
        {
            bool isValid = false;
            string userInput = "";
            int index = 0;

            while (!isValid)
            {
                Console.Write(indicationForUser);
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

        public static float GetAndValidGradeInput(string indicationForUser)
        {
            bool isValid = false;
            string userInput = "";
            float gradeValue = 0.0f;

            while (!isValid)
            {
                Console.Write(indicationForUser);
                userInput = Console.ReadLine();
                if (Single.TryParse(userInput, out gradeValue))
                {
                    if (gradeValue >= ConstantValue.MIN_GRADE && gradeValue <= ConstantValue.MAX_GRADE)
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine($"Vous devez entrer un réel compris entre {ConstantValue.MIN_GRADE} et {ConstantValue.MAX_GRADE}.\n");
                    }
                }
                else
                {
                    Console.WriteLine($"Vous devez entrer un réel compris entre {ConstantValue.MIN_GRADE} et {ConstantValue.MAX_GRADE}.\n");
                }
            }

            return gradeValue;
        }

        public static string GetAndValidNameInput(string indicationForUser)
        {
            bool isValid = false;
            string userInput = "";
            // Ne laisse pas passer les accents
            string pattern = ConstantValue.NAME_PATTERN;

            while (!isValid)
            {
                Console.Write(indicationForUser);
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

        public static string GetAndValidObservationInput(string indicationForUser)
        {
            bool isValid = false;
            string userInput = "";
            // Ne laisse pas passer les accents
            string pattern = ConstantValue.OBSERVATION_PATTERN;

            while (!isValid)
            {
                Console.Write(indicationForUser);
                userInput = Console.ReadLine();
                if (Regex.IsMatch(userInput, pattern) || userInput == string.Empty)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine(ConstantValue.OBSERVATION_ERROR_MESSAGE);
                }
            }

            return userInput;
        }

        public static DateTime GetAndValidDateInput(string indicationForUser)
        {
            bool isValid = false;
            string userInput;
            DateTime date = DateTime.MinValue;

            while (!isValid)
            {
                Console.Write(indicationForUser);
                userInput = Console.ReadLine();
                if (DateTime.TryParseExact(userInput, ConstantValue.DATE_FORMAT, null, DateTimeStyles.None, out date))
                {
                    int ageInDays = GetAgeInDays(date);
                    int age = GetAge(ageInDays);

                    if (ageInDays < 0)
                    {
                        Console.WriteLine(ConstantValue.AGE_ERROR_MESSAGE);
                    }
                    else if (age > 120.0f)
                    {
                        Console.WriteLine($"Vous ne pouvez pas avoir {age} ans (cf. Génèse 6:3).\n");
                    }
                    else
                    {
                        isValid = true;
                    }
                }
                else
                {
                    Console.WriteLine(ConstantValue.DATE_ERROR_MESSAGE);
                }
            }

            return date;
        }

        public static int GetAgeInDays(DateTime date)
        {
            return (DateTime.Now - date).Days;
        }

        public static int GetAge(int numberOfDays)
        {
            return (int)(numberOfDays / ConstantValue.YEAR);
        }
    }
}
