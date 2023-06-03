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
        private const string lineSeparator = "-----------------------------------------------------------------------------------------";

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


                    if (messages.Count > 0)
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
            Console.WriteLine($"Name(s): ".PadRight(50) + anniversaries[index].Names);
            Console.WriteLine($"Date: ".PadRight(50) + anniversaries[index].AnniversaryDate);
            Console.WriteLine($"Type: ".PadRight(50) + anniversaries[index].AnniversaryType);
            Console.WriteLine($"Description: ".PadRight(50) + anniversaries[index].Description);
            Console.WriteLine($"Email: ".PadRight(50) + anniversaries[index].Email);
            Console.WriteLine($"Phone: ".PadRight(50) + anniversaries[index].PhoneNumber);
            Console.WriteLine($"Address: ".PadRight(50) + anniversaries[index].Address.ToString());
            Console.WriteLine();

            Console.WriteLine(lineSeparator);
            DisplaySelectedOptions();

            string selectedOption = GetUserInput();
            if (selectedOption.Equals("E", StringComparison.OrdinalIgnoreCase))
                EditAnniversary(anniversaries, index);
            else if (selectedOption.Equals("D", StringComparison.OrdinalIgnoreCase))
                DeleteAnniversary(anniversaries, index);
            else if (selectedOption.Equals("M", StringComparison.OrdinalIgnoreCase))
                RedirectToMainMenu(anniversaries);
        }

        public void DisplayUpcomingAnniversary(in List<Anniversary> anniversaries)
        {
            Dictionary<int,DateTime> map = new Dictionary<int,DateTime>();

            for(int i=0;i<anniversaries.Count;i++)
            {
                DateTime nivDate = DateTime.Parse(anniversaries[i].AnniversaryDate);
                DateTime todayDate = new DateTime(nivDate.Year, DateTime.Now.Month, DateTime.Now.Day);

                TimeSpan difference = nivDate - todayDate;

                if (difference.Days <= 14 && difference.Days >= 0 )
                {
                    map.Add(i,nivDate);
                }
            }

            List<KeyValuePair<int, DateTime>> sortedList = map.OrderBy(x => x.Value.Month).ThenBy(x => x.Value.Day).ToList();

            DisplayHeader("Upcoming Anniversaries");
            Console.WriteLine("Name(s)\t\t\t\t\tDate\t\tType\t\tYears");
            Console.WriteLine(lineSeparator + "\n");
            for (int i=0;i<sortedList.Count;i++)
            {
                Anniversary niv = anniversaries[sortedList[i].Key];
                Console.WriteLine(niv + "\t"+(DateTime.Now.Year - sortedList[i].Value.Year));
            }
            Console.WriteLine(lineSeparator + "\n");
            Console.WriteLine("Press any key to continue...");
            while (!Console.KeyAvailable)
            {
            }
            Console.ReadKey();
            RedirectToMainMenu(anniversaries);
        }

        public void EditAnniversary(in List<Anniversary> anniversaries, int editIndex)
        {
            DisplayHeader("Edit Selected Anniversary");
            Console.WriteLine("\nKEY-IN NEW values for any field, or PRESS ENTER to accept the current field value...\n");

            string? input;
            Console.Write($"Names(s) \"{anniversaries[editIndex].Names}\": ");
            input = Console.ReadLine();
            anniversaries[editIndex].Names = string.IsNullOrEmpty(input) ? anniversaries[editIndex].Names : input!;

            Console.Write($"Anniversary Type \"{anniversaries[editIndex].AnniversaryType}\": ");
            input = Console.ReadLine();
            anniversaries[editIndex].AnniversaryType = string.IsNullOrEmpty(input) ? anniversaries[editIndex].AnniversaryType : input!;

            Console.Write($"Description \"{anniversaries[editIndex].Description}\": ");
            input = Console.ReadLine();
            anniversaries[editIndex].Description = string.IsNullOrEmpty(input) ? anniversaries[editIndex].Description : input!;

            Console.Write($"Anniversary Date \"{anniversaries[editIndex].AnniversaryDate}\": ");
            input = Console.ReadLine();
            anniversaries[editIndex].AnniversaryDate = string.IsNullOrEmpty(input) ? anniversaries[editIndex].AnniversaryDate : input!;

            Console.Write($"Email \"{anniversaries[editIndex].Email}\": ");
            input = Console.ReadLine();
            anniversaries[editIndex].Email = string.IsNullOrEmpty(input) ? anniversaries[editIndex].Email : input!;

            Console.Write($"Phone # \"{anniversaries[editIndex].PhoneNumber}\": ");
            input = Console.ReadLine();
            anniversaries[editIndex].PhoneNumber = string.IsNullOrEmpty(input) ? anniversaries[editIndex].PhoneNumber : input!;

            Console.Write($"Street Address \"{anniversaries[editIndex].Address.StreetAddress}\": ");
            input = Console.ReadLine();
            anniversaries[editIndex].Address.StreetAddress = string.IsNullOrEmpty(input) ? anniversaries[editIndex].Address.StreetAddress : input!;

            Console.Write($"Municipality \"{anniversaries[editIndex].Address.Municipality}\": ");
            input = Console.ReadLine();
            anniversaries[editIndex].Address.Municipality = string.IsNullOrEmpty(input) ? anniversaries[editIndex].Address.Municipality : input!;

            Console.Write($"Province \"{anniversaries[editIndex].Address.Province}\": ");
            input = Console.ReadLine();
            anniversaries[editIndex].Address.Province = string.IsNullOrEmpty(input) ? anniversaries[editIndex].Address.Province : input!;

            Console.Write($"PostalCode \"{anniversaries[editIndex].Address.PostalCode}\": ");
            input = Console.ReadLine();
            anniversaries[editIndex].Address.PostalCode = string.IsNullOrEmpty(input) ? anniversaries[editIndex].Address.PostalCode : input!;


            FileHandler.WriteLibToJsonFile(anniversaries, Program.JsonFile);
            RedirectToMainMenu(anniversaries);
        }

        public void DeleteAnniversary(in List<Anniversary> anniversaries, int deleteIndex)
        {
            anniversaries.Remove(anniversaries[deleteIndex]);
            FileHandler.WriteLibToJsonFile(anniversaries,Program.JsonFile);
            RedirectToMainMenu(anniversaries);
        }

        public void RedirectToMainMenu(in List<Anniversary> anniversaries)
        {
            DisplayHeader("All Anniversaries");
            DisplayAnniversaries(anniversaries);
            DisplayMainOptions();
        }



        private void DisplayHeader(string headerInfo)
        {
            Console.WriteLine(lineSeparator);
            Console.WriteLine($"\n\t\tANNIVERSARY MINDER ~ {headerInfo}\n");
            Console.WriteLine(lineSeparator);
        }

        private void DisplayMainOptions()
        {
            Console.WriteLine("\nPress # to choose an anniversary to view.");
            Console.WriteLine("Press N to add a new anniversary.");
            Console.WriteLine("Press U to list upcoming anniversary.");
            Console.WriteLine("Press X to quit.\n");
            Console.WriteLine(lineSeparator);
        }

        private void DisplaySelectedOptions()
        {
            Console.WriteLine("\nPress E to edit this anniversary.");
            Console.WriteLine("Press D to delete this anniversary.");
            Console.WriteLine("Press M to return to the main menu.\n");
            Console.WriteLine(lineSeparator);
        }

        public void Quit()
        {
            Environment.Exit(0);
        }

        public string GetUserInput()
        {
            return Console.ReadLine() ?? "";
        }
    }
}
