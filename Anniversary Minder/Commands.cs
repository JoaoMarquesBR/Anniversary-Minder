using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anniversary_Minder
{
    public class Commands
    {
        public Anniversary AddNewAnniversary()
        {
            Anniversary aniv = new Anniversary();

            Console.Write("Name: ");
            aniv.Names = GetUserInput();

            Console.Write("Date: ");
            aniv.Date = GetUserInput();

            Console.Write("Type: ");
            aniv.Type = GetUserInput();

            Console.Write("Description: ");
            aniv.Description = GetUserInput();

            Console.Write("Email: ");
            aniv.Email = GetUserInput();

            Console.Write("PhoneNumber: ");
            aniv.PhoneNumber = GetUserInput();

            return aniv;
        }

        public Address AddNewAddress()
        {
            Address address = new Address();

            Console.Write("Street Address: ");
            address.StreetAddress = GetUserInput();

            Console.Write("Municipality: ");
            address.Municipality = GetUserInput();

            Console.Write("Province: ");
            address.Province = GetUserInput();

            Console.Write("PostalCode: ");
            address.PostalCode = GetUserInput();

            return address;
        }

        public void ListUpcomingAnniversary()
        {
            Console.WriteLine("Upcoming anniversary");
        }

        public void Quit()
        {
            Console.WriteLine("Quit");
        }


        public void PrintCommandOptions()
        {
            Console.WriteLine("\nPress # from above list to entry.");
            Console.WriteLine("Press N to add a new anniversary.");
            Console.WriteLine("Press U to list upcoming anniversary.");
            Console.WriteLine("Press X to quit.");
        }

        public string GetUserInput()
        {
            string inputCommand = Console.ReadLine() ?? "";

            return inputCommand;
        }
    }
}
