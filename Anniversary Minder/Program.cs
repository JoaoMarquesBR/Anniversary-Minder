namespace Anniversary_Minder
{
    internal class Program
    {
        private const string JsonFile = @"../../../../anniversary.json";
        private const string SchemaFile = @"../../../../anniversary_schema.json";
        private const string lineSeparator = "-----------------------------------------------------------------------\n";

        static void Main(string[] args)
        {
            Commands cm = new Commands();

            Console.WriteLine(lineSeparator);
            Console.WriteLine("\t\tANNIVERSARY MINDER ~ All Anniversaries\n");
            Console.WriteLine(lineSeparator);

            List<Anniversary>? anniversaryList = cm.GetAnniversaries(JsonFile, SchemaFile);
            cm.DisplayAnniversaries(anniversaryList);

            cm.PrintCommandOptions();
            Console.WriteLine(lineSeparator);

            while (true)
            {
                string command = cm.GetUserInput().ToLower();
                command = command.Replace(" ", "");

                switch (command)
                {
                    case "n":
                        Anniversary anniversary = cm.AddAnniversary(SchemaFile);
                        anniversaryList.Add(anniversary);

                        FileHandler.WriteLibToJsonFile(anniversaryList, JsonFile);
                        break;

                    case "u":
                        cm.ListUpcomingAnniversary(anniversaryList);
                        break;

                    case "x":
                        cm.Quit();
                        break;

                    case "#":
                        cm.OfferUpdateAnniversary(anniversaryList);
                        break;

                   default:
                        Console.WriteLine("Invalid input");
                        break;
                }

            }
        }
    }
}