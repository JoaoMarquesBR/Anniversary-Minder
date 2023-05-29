namespace Anniversary_Minder
{
    internal class Program
    {
        const string JsonFile = @"../../../../anniversary.json";
        const string lineSeparator = "-----------------------------------------------------------------------\n";

        static void Main(string[] args)
        {
            Commands cm = new Commands();
            
            while (true)
            {
                Console.WriteLine(lineSeparator);
                Console.WriteLine("\t\tANNIVERSARY MINDER ~ All Anniversaries\n");
                Console.WriteLine(lineSeparator);
                cm.DisplayAnniversaries(JsonFile);
                cm.PrintCommandOptions();
                Console.WriteLine(lineSeparator);

                string command = cm.GetUserInput().ToLower();
                command = command.Replace(" ", "");

                switch (command)
                {
                    case "n":
                        Anniversary anniversary =  cm.AddAnniversary();
                        anniversary.Address = cm.AddAddress();

                        List<Anniversary> anniversaryList = FileHandler.ReadJsonFileToLib(JsonFile);
                        anniversaryList.Add(anniversary);
                        FileHandler.WriteLibToJsonFile(anniversaryList, JsonFile);
                        break;

                    case "u":
                        cm.ListUpcomingAnniversary();
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