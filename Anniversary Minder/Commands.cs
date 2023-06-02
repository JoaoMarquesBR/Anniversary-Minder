using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Anniversary_Minder
{
    public class Commands
    {
        public Anniversary AddAnniversary()
        {
            Anniversary anniv = new Anniversary();

            Console.Write("Name: ");
            anniv.Names = GetUserInput();

            Console.Write("AnniversaryDate: ");
            anniv.AnniversaryDate = GetUserInput();

            Console.Write("AnniversaryType: ");
            anniv.AnniversaryType = GetUserInput();

            Console.Write("Description: ");
            anniv.Description = GetUserInput();

            Console.Write("Email: ");
            anniv.Email = GetUserInput();

            Console.Write("PhoneNumber: ");
            anniv.PhoneNumber = GetUserInput();

            return anniv;
        }

        public Address AddAddress()
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

        public void DisplayAnniversaries(string file)
        {
            List<Anniversary>? anniversaries = FileHandler.ReadJsonFileToLib(file);

            Console.WriteLine("\nName(s)\t\t\t\t\tDate\t\tType\n");

            if (anniversaries != null)
            {
                int count = 1;
                foreach (Anniversary anniv in anniversaries)
                {
                    Console.WriteLine($"{count}.{anniv.Names}\t\t\t\t\t{anniv.AnniversaryDate}\t{anniv.AnniversaryType}");
                    count++;
                }
            }
            else
            {
                anniversaries = new List<Anniversary>();
            }

            Console.WriteLine("\n-----------------------------------------------------------------------");
        }

        public void ListUpcomingAnniversary(string path)
        {
            Console.WriteLine("Upcoming anniversary");
            List<Anniversary>? anniversaries = FileHandler.ReadJsonFileToLib(path);
            for(int i=0;i<anniversaries.Count;i++)
            {
                Console.Write((i+1)+": ");
                anniversaries[i].printInfo();
            }
        }

        public void offerUpdateAnniversary(string path)
        {
            Console.Write("Would you like to update any of these birthday?[Y/N]: ");
            string input = Console.ReadLine() ?? "";
            if (input.Contains("y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Select ID: ");
                input = Console.ReadLine();
                if (int.TryParse(input, out int idInputOuter))
                {
                    updateAnniversary(idInputOuter, path);

                }
                else
                {
                    Console.WriteLine("Error: Wrong input");
                    Console.WriteLine("Expected: Intenge");

                }
            }
        }

        public void updateAnniversary(int updateIndex,string path)
        {
            List<Anniversary>? anniversaries = FileHandler.ReadJsonFileToLib(path);
            Console.Write("Updating ");
            anniversaries[updateIndex].printInfo();
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
