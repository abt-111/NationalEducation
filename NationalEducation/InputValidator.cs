using System.Dynamic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NationalEducation
{
    internal static class InputValidator
    {
        // Récupérer et valider l'index entré par l'utilisateur
        public static int GetAndValidIndex(string indicationForUser, int maxIndex)
        {
            bool isValid = false;
            int index = 0;
            string userInput;

            // Tant que la saisie de l'utilisateur n'est pas valide
            while (!isValid)
            {
                Console.Write(indicationForUser);
                // Saisie Utilisateur
                userInput = Console.ReadLine();

                // Verifier de la validité de la saisie
                isValid = TryValidIndex(userInput, maxIndex, out index); // L'entrée utilisateur convertie est attribué à index

                // Si la saisie n'est pas valide
                if (!isValid)
                {
                    // Afficher un message d'erreur
                    Console.WriteLine($"Vous devez entrer un entier compris entre 0 et {maxIndex - 1}.\n");
                }
            }

            return index;
        }

        // Essayer de valider l'index entré par l'utilisateur
        public static bool TryValidIndex(string userInput, int maxIndex, out int index)
        {
            // Condition : userInput convertie en int et index compris entre 0 et maxIndex exclue
            // L'entrée utilisateur convertie est attribué à index si la première condition est vraie
            return Int32.TryParse(userInput, out index) && index >= 0 && index < maxIndex;
        }

        // Récupérer et valider la valeur de la note entrée par l'utilisateur
        public static float GetAndValidGradeValue(string indicationForUser)
        {
            bool isValid = false;
            string userInput = "";
            float gradeValue = 0.0f;

            // Tant que la saisie de l'utilisateur n'est pas valide
            while (!isValid)
            {
                Console.Write(indicationForUser);
                // Saisie Utilisateur
                userInput = Console.ReadLine();

                // Verifier de la validité de la saisie
                isValid = TryValidGradeValue(userInput, out gradeValue);

                // Si la saisie n'est pas valide
                if (!isValid)
                {
                    // Afficher un message d'erreur
                    Console.WriteLine($"Vous devez entrer un réel compris entre {ConstantValue.MIN_GRADE} et {ConstantValue.MAX_GRADE}.\n");
                }
            }

            return gradeValue;
        }

        // Essayer de valider la note entré par l'utilisateur
        public static bool TryValidGradeValue(string userInput, out float gradeValue)
        {
            // Condition : userInput convertie en float et gradeValue compris entre ConstantValue.MIN_GRADE et ConstantValue.MAX_GRADE
            // L'entrée utilisateur convertie est attribué à gradeValue si la première condition est vraie
            return Single.TryParse(userInput, out gradeValue) && gradeValue >= ConstantValue.MIN_GRADE && gradeValue <= ConstantValue.MAX_GRADE;
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
