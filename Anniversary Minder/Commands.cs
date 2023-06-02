using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
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
        public Anniversary AddAnniversary(in string SchemaFile)
        {
            Anniversary anniv = new Anniversary();
            bool valid;

            do
            {
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

                anniv.Address = AddAddress();

                valid = ValidateItem(anniv, SchemaFile);

            } while (valid == false);

            return anniv;
        }

        private Address AddAddress()
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

        public void DisplayAnniversaries(in List<Anniversary> anniversaries)
        {
            Console.WriteLine("Name(s)\t\t\t\t\tDate\t\tType\n");

            int count = 1;
            foreach (Anniversary anniv in anniversaries)
            {
                Console.WriteLine($"{count}.{anniv.Names}\t\t\t\t\t{anniv.AnniversaryDate}\t{anniv.AnniversaryType}");
                count++;
            }

            Console.WriteLine("\n-----------------------------------------------------------------------");
        }


        public List<Anniversary> GetAnniversaries(in string JsonFile, in string SchemaFile)
        {
            List<Anniversary>? anniversaries = FileHandler.ReadJsonToAnniversary(JsonFile, SchemaFile);

            if (anniversaries == null)
                anniversaries = new List<Anniversary>();

            return anniversaries!;
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
            Console.WriteLine("Press X to quit.\n");
        }

        public string GetUserInput()
        {
            string inputCommand = Console.ReadLine() ?? "";

            return inputCommand;
        }

        private static bool ValidateItem(in Anniversary item, in string jsonSchema)
        {
            string jsonData = JsonConvert.SerializeObject(item);
            JObject itemObj = JObject.Parse(jsonData);
            JSchema schema = JSchema.Parse(jsonSchema);

            return itemObj.IsValid(schema);
        }
    }
}
