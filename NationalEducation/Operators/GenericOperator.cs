using NationalEducation.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalEducation.Operators
{
    internal static class GenericOperator
    {
        // Lister les éléments IListable
        public static void DisplayItemsOfList<T>(List<T> ListOfT, string listDescription, string noListDescription) where T : IListable
        {
            if (ListOfT.Count > 0)
            {
                Console.WriteLine($"{listDescription}\n");

                int index = 0;
                foreach (T t in ListOfT)
                {
                    Console.WriteLine($"{index} - {t.Name}");
                    index++;
                }

                Log.Information($"Consultation de {listDescription}");
            }
            else
            {
                Console.WriteLine(noListDescription);

                Log.Information($"Échec de la consultation. {noListDescription}");
            }

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        public static T SelectItemOfList<T>(List<T> ListOfT, string selectDescription)
        {
            int index;

            index = InputValidator.GetAndValidIndexInput(selectDescription, ListOfT.Count);

            return ListOfT[index];
        }

        public static uint GenerateId<T>(List<T> ListOfT) where T : IIdentifiable
        {
            if (ListOfT.Count == 0)
                return 0;
            else
                return ListOfT.Last().Id + 1;
        }
    }
}
