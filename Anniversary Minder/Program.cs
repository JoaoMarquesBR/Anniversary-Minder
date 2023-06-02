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
            
            while (true)
            {
                Console.WriteLine(lineSeparator);
                Console.WriteLine("\t\tANNIVERSARY MINDER ~ All Anniversaries\n");
                Console.WriteLine(lineSeparator);

                List<Anniversary>? anniversaryList = cm.GetAnniversaries(JsonFile, SchemaFile);
                cm.DisplayAnniversaries(anniversaryList);

                cm.PrintCommandOptions();
                Console.WriteLine(lineSeparator);

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
                        cm.ListUpcomingAnniversary(JsonFile);
                        cm.offerUpdateAnniversary(JsonFile);
                        break;

                    case "x":
                        cm.Quit();
                        break;

                    case "#":
                        
                        break;

                   default:
                        Console.WriteLine("Invalid input");
                        break;
                }

            }
        }
    }
}