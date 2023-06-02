using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anniversary_Minder
{
    public class Commands
    {
        private const string lineSeparator = "-----------------------------------------------------------------------";

        public Anniversary AddAnniversary(in string SchemaFile)
        {
            Anniversary anniv = new Anniversary();

            if (FileHandler.ReadFile(SchemaFile, out string jsonSchema))
            {
                bool valid;
                do
                {
                    Console.WriteLine("\nPlease key-in values for the following fields...\n");

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

                    valid = ValidateItem(anniv, jsonSchema, out IList<string> messages);

                    if (messages != null)
                    {
                        Console.WriteLine($"\nERROR:\tInvalid anniversary information entered.\n");

                        // Report validation error messages
                        foreach (string msg in messages)
                            Console.WriteLine($"\t{msg}");

                        Console.WriteLine($"\nEnter the information again with valid data.\n");

                    }
                } while (valid == false);
            }

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

        private static bool ValidateItem(in Anniversary item, in string jsonSchema, out IList<string> messages)
        {
            string jsonData = JsonConvert.SerializeObject(item);
            JObject itemObj = JObject.Parse(jsonData);
            JSchema schema = JSchema.Parse(jsonSchema);

            return itemObj.IsValid(schema, out messages);
        }

        public List<Anniversary> GetAnniversaries(in string JsonFile, in string SchemaFile)
        {
            List<Anniversary>? anniversaries = FileHandler.ReadJsonToAnniversary(JsonFile, SchemaFile);

            if (anniversaries == null)
                anniversaries = new List<Anniversary>();

            return anniversaries!;
        }

        public void DisplayAnniversaries(in List<Anniversary> anniversaries)
        {
            Console.WriteLine("Name(s)\t\t\t\t\tDate\t\tType");
            Console.WriteLine(lineSeparator + "\n");

            int count = 1;
            foreach (Anniversary anniv in anniversaries)
            {
                Console.WriteLine($"{count}. {anniv.ToString()}");
                count++;
            }

            Console.WriteLine("\n" + lineSeparator);
        }

        public void DisplaySelectedAnniversary(in List<Anniversary> anniversaries)
        {
            Console.Write("\nPress a number from the above list to entry -> ");
            int index = Convert.ToInt32(GetUserInput()) - 1;

            Console.WriteLine();
            DisplayHeader("Selected Anniversary");

            Console.WriteLine();
            Console.WriteLine("Names: " + anniversaries[index].Names);
            Console.WriteLine("Date: " + anniversaries[index].AnniversaryDate);
            Console.WriteLine("Type: " + anniversaries[index].Names);
            Console.WriteLine("Description: " + anniversaries[index].Description);
            Console.WriteLine("Email: " + anniversaries[index].Email);
            Console.WriteLine("Phone: " + anniversaries[index].PhoneNumber);
            Console.WriteLine("Address: " + anniversaries[index].Address.ToString());
            Console.WriteLine();

            Console.WriteLine(lineSeparator);
            DisplaySelectedOptions();
        }

        public void ListUpcomingAnniversary(in List<Anniversary> anniversaries)
        {
            
        }

        //TODO -> Instead of OfferUpdateAnniversary we need to allow user to choose
        // which anniversary he wants to see, and we also need to give them the
        // options to Edit, Delete and Return (to the list of anniversaries)
        public void OfferUpdateAnniversary(in List<Anniversary> anniversaries)
        {
            Console.Write("Would you like to update any of these birthday? [Y/N]: ");
            string input = Console.ReadLine() ?? "";

            if (input.Contains("y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Select ID: ");
                input = Console.ReadLine() ?? "";
                if (int.TryParse(input, out int annivIndex))
                {
                    UpdateAnniversary(anniversaries, annivIndex);
                }
                else
                {
                    Console.WriteLine("Error: Wrong input");
                    Console.WriteLine("Expected: Integer");

                }
            }
        }

        public void UpdateAnniversary(in List<Anniversary> anniversaries, int updateIndex)
        {
            
        }

        

        public void DisplayHeader(string headerInfo)
        {
            Console.WriteLine(lineSeparator);
            Console.WriteLine($"\n\t\tANNIVERSARY MINDER ~ {headerInfo}\n");
            Console.WriteLine(lineSeparator);
        }

        public void DisplayMainOptions()
        {
            Console.WriteLine("\nPress # to choose an anniversary to view.");
            Console.WriteLine("Press N to add a new anniversary.");
            Console.WriteLine("Press U to list upcoming anniversary.");
            Console.WriteLine("Press X to quit.\n");
            Console.WriteLine(lineSeparator);
        }

        public void DisplaySelectedOptions()
        {
            Console.WriteLine("\nPress E to edit this anniversary.");
            Console.WriteLine("Press D to delete this anniversary.");
            Console.WriteLine("Press E to edit this anniversary.");
            Console.WriteLine("Press M to return to the main menu.\n");
            Console.WriteLine(lineSeparator);
        }

        public void Quit()
        {
            Console.WriteLine("Quit");
        }

        public string GetUserInput()
        {
            return Console.ReadLine() ?? "";
        }
    }
}
