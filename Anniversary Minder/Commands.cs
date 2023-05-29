using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
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
